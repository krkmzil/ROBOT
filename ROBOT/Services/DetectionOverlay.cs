using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ROBOT.Services
{
    // Nesne bulma ve yeşil çerçeveyi çizme: hem Görüntü İşleme hem Kamera Ayarları sayfası kullanır.
    public static class DetectionOverlay
    {
        // Perimeter²'ye oranla alan: mükemmel daire için 1.0, kare için ~0.785,
        // uzun/oval şekiller için çok daha düşük. Bu eşiğin üstü "yuvarlak" sayılır.
        public const double CircularityThreshold = 0.8;

        private static readonly Color FrameColor = Color.FromArgb(76, 175, 80);

        // forcedShape verilirse bütün çerçeveler o şekille çizilir (Kamera Ayarları'nda seçili şekil);
        // verilmezse tanınan örneğin kendi şekli kullanılır.
        // includeUnmatched: hiçbir örnekle eşleşmeyen nesneler de (etiketsiz) çizilsin mi.
        public static List<Detection> Detect(
            ObjectRecognitionService service,
            Bitmap crop,
            IReadOnlyList<ObjectSample> samples,
            ObjectShape? forcedShape,
            bool includeUnmatched)
        {
            var detections = new List<Detection>();

            using var mat = BitmapConverter.ToMat(crop);
            var contours = service.DetectBlobs(mat);

            foreach (var contour in contours)
            {
                ClassificationResult? match = null;
                if (samples.Count > 0 && service.IsModelLoaded)
                {
                    using var blobMat = new Mat(mat, Cv2.BoundingRect(contour));
                    var embedding = service.ComputeEmbedding(blobMat);
                    match = embedding != null ? service.Classify(embedding, samples) : null;
                }

                if (match == null && !includeUnmatched) continue; // Eşleşmeyenler için varsayılan olarak hiçbir şey çizilmez.

                ObjectShape shape = forcedShape ?? match?.Shape ?? ObjectShape.Auto;

                // Ham kontur, eşikleme gürültüsünden dolayı küçük pürüzler/çentikler içerebilir;
                // dışbükey gövde (convex hull) kenarları düzleştirip ölçümleri stabilize eder.
                var hull = Cv2.ConvexHull(contour);
                double area = Cv2.ContourArea(hull);
                double perimeter = Cv2.ArcLength(hull, true);
                double circularity = perimeter > 0 ? (4 * Math.PI * area) / (perimeter * perimeter) : 0;

                Cv2.MinEnclosingCircle(hull, out var circleCenter, out float radius);

                var rotatedRect = Cv2.MinAreaRect(hull);
                float width = rotatedRect.Size.Width;
                float height = rotatedRect.Size.Height;
                float angle = rotatedRect.Angle;
                if (width < height)
                {
                    (width, height) = (height, width);
                    angle += 90;
                }
                angle = ((angle % 180) + 180) % 180;

                bool isCircle = shape switch
                {
                    ObjectShape.Circle => true,
                    ObjectShape.Rectangle => false,
                    _ => circularity > CircularityThreshold
                };

                detections.Add(new Detection
                {
                    ClassName = match?.ClassName ?? "",
                    Matched = match != null,
                    ScorePercent = match != null ? match.Score * 100 : 0,
                    ShapeMode = shape,
                    Circularity = circularity,
                    IsCircle = isCircle,
                    CircleX = circleCenter.X,
                    CircleY = circleCenter.Y,
                    Radius = radius,
                    RectX = rotatedRect.Center.X,
                    RectY = rotatedRect.Center.Y,
                    RectWidth = width,
                    RectHeight = height,
                    RectAngle = angle
                });
            }

            return detections;
        }

        // offsetX/offsetY: bulunan konumların çizilecek görüntüdeki başlangıcı (ROI kırpmasının sol üst köşesi).
        public static void Draw(Graphics g, IEnumerable<Detection> detections, float offsetX, float offsetY)
        {
            using var pen = new Pen(FrameColor, 2);
            using var font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var bgBrush = new SolidBrush(FrameColor);

            foreach (var d in detections)
            {
                float labelX, labelTopY;

                if (d.IsCircle)
                {
                    g.DrawEllipse(pen, offsetX + d.CircleX - d.Radius, offsetY + d.CircleY - d.Radius, d.Radius * 2, d.Radius * 2);
                    labelX = offsetX + d.CircleX - d.Radius;
                    labelTopY = offsetY + d.CircleY - d.Radius;
                }
                else
                {
                    var corners = d.GetRectCorners().Select(p => new PointF(p.X + offsetX, p.Y + offsetY)).ToArray();
                    g.DrawPolygon(pen, corners);
                    labelX = corners.Min(p => p.X);
                    labelTopY = corners.Min(p => p.Y);
                }

                string label = d.Label;
                if (label.Length == 0) continue;

                var textSize = g.MeasureString(label, font);
                float labelY = Math.Max(0, labelTopY - textSize.Height);
                g.FillRectangle(bgBrush, labelX, labelY, textSize.Width + 6, textSize.Height);
                g.DrawString(label, font, Brushes.White, labelX + 3, labelY);
            }
        }
    }
}
