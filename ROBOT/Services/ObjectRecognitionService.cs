using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;

namespace ROBOT.Services
{
    // Görüntü İşleme'de nesnenin etrafına çizilecek çerçevenin şekli.
    public enum ObjectShape
    {
        Auto,       // Nesnenin ne kadar yuvarlak olduğuna bakılarak otomatik seçilir
        Circle,     // Daire
        Rectangle   // Nesnenin açısına oturan (döndürülmüş) dikdörtgen
    }

    public class ObjectSample
    {
        public string ClassName { get; set; } = "";
        public float[] Embedding { get; set; } = Array.Empty<float>();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ObjectShape Shape { get; set; } = ObjectShape.Auto;

        // Veri setindeki örnek resminin, veri seti klasörüne göre yolu (ör. "Elma\20261007_153012_123.png").
        public string? ImageFile { get; set; }
    }

    public class ClassificationResult
    {
        public string ClassName { get; set; } = "";
        public float Score { get; set; }
        public ObjectShape Shape { get; set; } = ObjectShape.Auto;
    }

    // Kamerada gösterilen nesneleri "öğrenip" (birkaç örnek kaydederek) daha sonra
    // canlı görüntüde tanıyabilmek için: önce zemin/nesne ayrımı yaparak nesnenin
    // bulunduğu bölgeyi (blob) bulur, sonra o bölgeyi önceden eğitilmiş bir görüntü
    // modelinden geçirip sayısal bir "parmak izi" (embedding) çıkarır. Tanıma, yeni
    // bir embedding'i kayıtlı örneklerle karşılaştırıp en yakınını bulmakla yapılır
    // (model burada yeniden eğitilmiyor — sadece öznitelik çıkarıcı olarak kullanılıyor).
    public class ObjectRecognitionService : IDisposable
    {
        private const int ModelInputSize = 224;
        private const float RecognitionThreshold = 0.75f;
        private const int MinBlobArea = 800;

        private static readonly float[] ImageNetMean = { 0.485f, 0.456f, 0.406f }; // R, G, B
        private static readonly float[] ImageNetStd = { 0.229f, 0.224f, 0.225f };

        private readonly string _modelPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "mobilenetv2.onnx");

        private InferenceSession? _session;

        public bool IsModelLoaded => _session != null;

        public ObjectRecognitionService()
        {
            TryLoadModel();
        }

        private void TryLoadModel()
        {
            try
            {
                if (File.Exists(_modelPath))
                    _session = new InferenceSession(_modelPath);
            }
            catch
            {
                _session = null;
            }
        }

        // Kare içindeki zeminden ayrışan bölgeleri (nesneleri) bulur.
        // Otsu eşiklemesi iki baskın parlaklık kümesi bulur; alanca büyük olan
        // muhtemelen zemindir, bu yüzden daha küçük olan taraf nesne kabul edilir.
        // Her blob için ham konturu döner (sadece sınırlayıcı dikdörtgen değil) — çağıran
        // taraf buradan hem kırpma dikdörtgenini hem de şekle uygun çizim (daire/döndürülmüş
        // dikdörtgen) için gereken geometriyi çıkarabilir.
        public List<OpenCvSharp.Point[]> DetectBlobs(Mat bgrFrame)
        {
            var result = new List<OpenCvSharp.Point[]>();
            if (bgrFrame.Empty()) return result;

            using var gray = new Mat();
            Cv2.CvtColor(bgrFrame, gray, ColorConversionCodes.BGR2GRAY);

            // Kamera gürültüsü eşiklemede nesne sınırını her karede oynattığı için önce hafifçe bulanıklaştırılır.
            Cv2.GaussianBlur(gray, gray, new OpenCvSharp.Size(7, 7), 0);

            using var thresh = new Mat();
            Cv2.Threshold(gray, thresh, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

            double whiteRatio = Cv2.CountNonZero(thresh) / (double)(thresh.Rows * thresh.Cols);
            if (whiteRatio > 0.5)
                Cv2.BitwiseNot(thresh, thresh);

            using var kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(7, 7));
            using var cleaned = new Mat();
            Cv2.MorphologyEx(thresh, cleaned, MorphTypes.Open, kernel);
            Cv2.MorphologyEx(cleaned, cleaned, MorphTypes.Close, kernel);

            Cv2.FindContours(cleaned, out OpenCvSharp.Point[][] contours, out _, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            foreach (var contour in contours)
            {
                if (Cv2.ContourArea(contour) < MinBlobArea) continue;
                result.Add(contour);
            }

            return result;
        }

        public float[]? ComputeEmbedding(Mat bgrCrop)
        {
            if (_session == null || bgrCrop.Empty()) return null;

            try
            {
                float[] tensorData = ExtractChwTensor(bgrCrop);
                var inputTensor = new DenseTensor<float>(tensorData, new[] { 1, 3, ModelInputSize, ModelInputSize });
                string inputName = _session.InputMetadata.Keys.First();
                var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor(inputName, inputTensor) };

                using var results = _session.Run(inputs);
                return results.First().AsEnumerable<float>().ToArray();
            }
            catch
            {
                return null;
            }
        }

        // samples parametresi çağıran tarafından verilir: her kamera profili kendi
        // öğrettiği nesne örneklerini taşır, tanıma o profilin listesiyle sınırlı kalır.
        public ClassificationResult? Classify(float[] embedding, IReadOnlyList<ObjectSample> samples)
        {
            ObjectSample? best = null;
            float bestScore = -1f;

            foreach (var sample in samples)
            {
                float score = CosineSimilarity(embedding, sample.Embedding);
                if (score > bestScore)
                {
                    bestScore = score;
                    best = sample;
                }
            }

            if (best == null || bestScore < RecognitionThreshold) return null;
            return new ClassificationResult { ClassName = best.ClassName, Score = bestScore, Shape = best.Shape };
        }

        private static float[] ExtractChwTensor(Mat bgrMat)
        {
            const int size = ModelInputSize;

            using var resized = new Mat();
            Cv2.Resize(bgrMat, resized, new OpenCvSharp.Size(size, size));

            using var continuous = resized.Clone(); // Marshal.Copy güvenli sürekli bellek ister.
            int channels = continuous.Channels();
            int totalBytes = size * size * channels;
            byte[] raw = new byte[totalBytes];
            Marshal.Copy(continuous.Data, raw, 0, totalBytes);

            var tensor = new float[3 * size * size];
            int planeSize = size * size;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    int pixelOffset = (y * size + x) * channels;
                    byte b = raw[pixelOffset + 0];
                    byte g = raw[pixelOffset + 1];
                    byte r = raw[pixelOffset + 2];

                    int idx = y * size + x;
                    tensor[0 * planeSize + idx] = ((r / 255f) - ImageNetMean[0]) / ImageNetStd[0];
                    tensor[1 * planeSize + idx] = ((g / 255f) - ImageNetMean[1]) / ImageNetStd[1];
                    tensor[2 * planeSize + idx] = ((b / 255f) - ImageNetMean[2]) / ImageNetStd[2];
                }
            }

            return tensor;
        }

        private static float CosineSimilarity(float[] a, float[] b)
        {
            if (a.Length != b.Length || a.Length == 0) return -1f;

            float dot = 0, magA = 0, magB = 0;
            for (int i = 0; i < a.Length; i++)
            {
                dot += a[i] * b[i];
                magA += a[i] * a[i];
                magB += b[i] * b[i];
            }

            if (magA == 0 || magB == 0) return -1f;
            return dot / (MathF.Sqrt(magA) * MathF.Sqrt(magB));
        }

        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}
