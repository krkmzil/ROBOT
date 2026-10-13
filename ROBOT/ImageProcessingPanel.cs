using OpenCvSharp;
using OpenCvSharp.Extensions;
using ROBOT.Services;

namespace ROBOT
{
    public partial class ImageProcessingPanel : UserControl
    {
        private readonly CameraSettingsService _settingsService = new();
        private readonly CameraService _camera = new();
        private readonly ObjectRecognitionService _recognitionService = new();
        private readonly DetectionTracker _tracker = new();

        private bool _resumeOnShow = false;

        public string? ActiveProfileName { get; private set; }
        public CameraSettingsData? ActiveProfileData { get; private set; }

        public ImageProcessingPanel()
        {
            InitializeComponent();
            _camera.FrameReceived += Camera_FrameReceived;
        }

        // Uygulama kapanınca çağrılır; kamerayı ve tanıma modelini güvenle kapatır.
        public void Cleanup()
        {
            ReleaseCamera();
            _recognitionService.Dispose();
        }

        // Shell sayfadan ayrılırken çağırır: seçili profil varsa kamerayı bırakır ama profili hatırlar.
        public void SuspendCamera()
        {
            _resumeOnShow = ActiveProfileData?.CameraName != null;
            ReleaseCamera();
        }

        // Shell sayfaya geri dönüldüğünde çağırır: aktif profili yeniden uygulayıp kamerayı açar.
        public void ResumeCamera()
        {
            if (!_resumeOnShow) return;
            _resumeOnShow = false;

            ApplyActiveProfile();
        }

        // Kamera Ayarları sayfası aynı fiziksel kameraya ihtiyaç duyabilir; sayfalar
        // arası geçişte shell bunu çağırarak donanım çakışmasını önler.
        public void ReleaseCamera()
        {
            _camera.Disconnect();
            _tracker.Reset();
            pictureBoxPreview.Image?.Dispose();
            pictureBoxPreview.Image = null;
        }

        private void buttonSelectProfile_Click(object sender, EventArgs e)
        {
            var profileNames = _settingsService.GetProfileNames();

            if (profileNames.Count == 0)
            {
                MessageBox.Show("Henüz kaydedilmiş bir profil yok. Önce Kamera Ayarları'ndan bir profil kaydet.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var selectForm = new FormSelectProfile(profileNames);
            if (selectForm.ShowDialog(FindForm()) != DialogResult.OK) return;

            ActiveProfileName = selectForm.SelectedProfileName;
            ActiveProfileData = ActiveProfileName != null ? _settingsService.LoadProfile(ActiveProfileName) : null;

            int sampleCount = ActiveProfileData?.ObjectSamples?.Count ?? 0;
            labelActiveProfile.Text = sampleCount > 0
                ? $"Aktif profil: {ActiveProfileName} ({sampleCount} öğretilmiş örnek)"
                : $"Aktif profil: {ActiveProfileName}";

            ApplyActiveProfile();
        }

        private void ApplyActiveProfile()
        {
            _camera.Disconnect();
            _tracker.Reset();
            pictureBoxPreview.Image?.Dispose();
            pictureBoxPreview.Image = null;

            if (ActiveProfileData?.CameraName == null) return;

            bool ok = _camera.Connect(ActiveProfileData.CameraName);
            if (!ok)
            {
                MessageBox.Show($"'{ActiveProfileData.CameraName}' kamerasına bağlanılamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ApplyIfPresent(CameraProperty.Brightness, ActiveProfileData.Brightness);
            ApplyIfPresent(CameraProperty.Contrast, ActiveProfileData.Contrast);
            ApplyIfPresent(CameraProperty.Sharpness, ActiveProfileData.Sharpness);
            ApplyIfPresent(CameraProperty.Saturation, ActiveProfileData.Saturation);
        }

        private void ApplyIfPresent(CameraProperty property, int? value)
        {
            if (value != null) _camera.SetProperty(property, value.Value);
        }

        // ---- Kamera karesi işleme ----

        private void Camera_FrameReceived(Bitmap frame)
        {
            Bitmap processed = ProcessFrame(frame);
            DisplayFrame(processed);
        }

        private Bitmap ProcessFrame(Bitmap frame)
        {
            Bitmap toShow = frame;

            if (ActiveProfileData?.RoiWidth is int w && ActiveProfileData?.RoiHeight is int h &&
                ActiveProfileData?.RoiX is int x && ActiveProfileData?.RoiY is int y)
            {
                int cropX = Math.Clamp(x, 0, Math.Max(0, frame.Width - 1));
                int cropY = Math.Clamp(y, 0, Math.Max(0, frame.Height - 1));
                int cropW = Math.Clamp(w, 1, frame.Width - cropX);
                int cropH = Math.Clamp(h, 1, frame.Height - cropY);

                var cropRect = new Rectangle(cropX, cropY, cropW, cropH);
                toShow = frame.Clone(cropRect, frame.PixelFormat);
                frame.Dispose();
            }

            // Bu profil için öğretilmiş nesne varsa, seçili profil ne öğretildiyse
            // sadece onu tanır; eşleşme yoksa hiçbir şey çizmez.
            if (ActiveProfileData?.ObjectSamples is { Count: > 0 } samples)
                toShow = AnnotateWithRecognition(toShow, samples);

            return toShow;
        }

        private Bitmap AnnotateWithRecognition(Bitmap crop, List<ObjectSample> samples)
        {
            try
            {
                // Sadece bu profilde öğretilmiş nesneler çizilir; çerçeve şekli öğretilirken seçilen şekildir.
                var raw = DetectionOverlay.Detect(_recognitionService, crop, samples, forcedShape: null, includeUnmatched: false);

                // Kare kare titremeyi önlemek için nesneler takip edilip konum/boyut/yüzde yumuşatılır.
                var smoothed = _tracker.Update(raw);

                using var g = Graphics.FromImage(crop);
                DetectionOverlay.Draw(g, smoothed, 0, 0);
            }
            catch
            {
                // Kare işlenirken bir sorun olursa canlı görüntüyü kesmeden ham kareyi göster.
            }

            return crop;
        }

        private void DisplayFrame(Bitmap processed)
        {
            if (InvokeRequired)
            {
                // BeginInvoke: Invoke kullanılırsa Bağlantı Kes/profil değiştirme sırasında
                // kilitlenme riski var (bkz. CameraSettingsPanel'deki aynı not).
                if (IsDisposed) { processed.Dispose(); return; }
                try
                {
                    BeginInvoke(new Action<Bitmap>(DisplayFrame), processed);
                }
                catch (ObjectDisposedException)
                {
                    processed.Dispose();
                }
                return;
            }

            var oldImage = pictureBoxPreview.Image;
            pictureBoxPreview.Image = processed;
            oldImage?.Dispose();
        }
    }
}
