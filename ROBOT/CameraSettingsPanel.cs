using OpenCvSharp;
using OpenCvSharp.Extensions;
using ROBOT.Services;

namespace ROBOT
{
    public partial class CameraSettingsPanel : UserControl
    {
        private readonly CameraService _camera = new();
        private readonly CameraSettingsService _settingsService = new();
        private readonly ObjectRecognitionService _recognitionService = new();

        private const int RoiNudgeStep = 10;
        private bool _roiInitialized = false;
        private int _frameWidth;
        private int _frameHeight;
        private int _roiX;
        private int _roiY;
        private int _roiWidth = 320;
        private int _roiHeight = 240;

        private bool _resumeOnShow = false;
        private bool _pendingSliderRestore = false;
        private int?[] _savedSliderValues = new int?[4];

        private readonly List<ObjectSample> _objectSamples = new();
        private readonly object _cropLock = new();
        private Bitmap? _latestRoiCrop;

        public CameraSettingsPanel()
        {
            InitializeComponent();
            _camera.FrameReceived += Camera_FrameReceived;
            comboBoxCamera.DropDown += (_, __) => RefreshCameraList();
        }

        private void CameraSettingsPanel_Load(object sender, EventArgs e)
        {
            RefreshCameraList();
        }

        // Uygulama kapanınca çağrılır; kamerayı ve tanıma modelini güvenle kapatır.
        public void Cleanup()
        {
            ReleaseCamera();

            lock (_cropLock)
            {
                _latestRoiCrop?.Dispose();
                _latestRoiCrop = null;
            }

            _recognitionService.Dispose();
        }

        // Hem ilk yüklemede hem de dropdown her açıldığında çağrılır, böylece
        // uygulama açıkken sonradan takılan USB kameralar da listeye düşer.
        private void RefreshCameraList()
        {
            string? previouslySelected = comboBoxCamera.SelectedItem?.ToString();

            comboBoxCamera.Items.Clear();
            comboBoxCamera.Items.AddRange(_camera.GetAvailableCameras());

            if (previouslySelected != null && comboBoxCamera.Items.Contains(previouslySelected))
                comboBoxCamera.SelectedItem = previouslySelected;
            else if (comboBoxCamera.Items.Count > 0)
                comboBoxCamera.SelectedIndex = 0;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            ConnectSelectedCamera(restoreSettings: false, silent: false);
        }

        // restoreSettings: sayfaya geri dönüşte otomatik yeniden bağlanırken, önceki slider
        // değerlerini ve ROI konumunu korur (manuel "Bağlan"da her şey sıfırdan başlar).
        private bool ConnectSelectedCamera(bool restoreSettings, bool silent)
        {
            if (comboBoxCamera.SelectedItem == null)
            {
                if (!silent)
                    MessageBox.Show("Önce bir kamera seç.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string cameraName = comboBoxCamera.SelectedItem.ToString()!;

            if (!_camera.Connect(cameraName))
            {
                if (!silent)
                    MessageBox.Show("Kameraya bağlanamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!restoreSettings)
                _roiInitialized = false;

            UpdateConnectionUi();
            SetupPropertySlider(trackBarBrightness, labelBrightnessValue, CameraProperty.Brightness);
            SetupPropertySlider(trackBarContrast, labelContrastValue, CameraProperty.Contrast);
            SetupPropertySlider(trackBarSharpness, labelSharpnessValue, CameraProperty.Sharpness);
            SetupPropertySlider(trackBarSaturation, labelSaturationValue, CameraProperty.Saturation);

            if (restoreSettings)
            {
                RestoreSliderValues();
                _pendingSliderRestore = true; // sürücü akış başlayınca değerleri sıfırlarsa ilk karede tekrar uygula
            }

            return true;
        }

        private void RestoreSliderValues()
        {
            RestoreSlider(trackBarBrightness, labelBrightnessValue, CameraProperty.Brightness, _savedSliderValues[0]);
            RestoreSlider(trackBarContrast, labelContrastValue, CameraProperty.Contrast, _savedSliderValues[1]);
            RestoreSlider(trackBarSharpness, labelSharpnessValue, CameraProperty.Sharpness, _savedSliderValues[2]);
            RestoreSlider(trackBarSaturation, labelSaturationValue, CameraProperty.Saturation, _savedSliderValues[3]);
        }

        private void RestoreSlider(TrackBar trackBar, Label valueLabel, CameraProperty property, int? savedValue)
        {
            if (savedValue == null || !trackBar.Enabled) return;

            trackBar.Value = Math.Clamp(savedValue.Value, trackBar.Minimum, trackBar.Maximum);
            _camera.SetProperty(property, trackBar.Value);
            valueLabel.Text = trackBar.Value.ToString();
        }

        // Shell sayfadan ayrılırken çağırır: bağlıysa slider değerlerini hatırlayıp kamerayı bırakır.
        public void SuspendCamera()
        {
            _resumeOnShow = _camera.IsConnected;
            if (!_resumeOnShow) return;

            _savedSliderValues = new int?[]
            {
                trackBarBrightness.Enabled ? trackBarBrightness.Value : null,
                trackBarContrast.Enabled ? trackBarContrast.Value : null,
                trackBarSharpness.Enabled ? trackBarSharpness.Value : null,
                trackBarSaturation.Enabled ? trackBarSaturation.Value : null
            };

            ReleaseCamera();
        }

        // Shell sayfaya geri dönüldüğünde çağırır: ayrılırken bağlıysa otomatik yeniden bağlanır.
        public void ResumeCamera()
        {
            if (!_resumeOnShow) return;
            _resumeOnShow = false;

            ConnectSelectedCamera(restoreSettings: true, silent: true);
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            if (!_camera.IsConnected)
            {
                MessageBox.Show("Önce kameraya bağlan.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var inputForm = new FormInputName("Bu profile bir isim ver:");
            if (inputForm.ShowDialog(FindForm()) != DialogResult.OK) return;

            string profileName = inputForm.EnteredName;

            var data = new CameraSettingsData
            {
                CameraName = comboBoxCamera.SelectedItem?.ToString(),
                Brightness = trackBarBrightness.Enabled ? trackBarBrightness.Value : null,
                Contrast = trackBarContrast.Enabled ? trackBarContrast.Value : null,
                Sharpness = trackBarSharpness.Enabled ? trackBarSharpness.Value : null,
                Saturation = trackBarSaturation.Enabled ? trackBarSaturation.Value : null,
                RoiX = _roiX,
                RoiY = _roiY,
                RoiWidth = _roiWidth,
                RoiHeight = _roiHeight,
                ObjectSamples = _objectSamples.Count > 0 ? new List<ObjectSample>(_objectSamples) : null
            };

            _settingsService.SaveProfile(profileName, data);
            MessageBox.Show($"'{profileName}' profili kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Bu profil kaydedildi; bir sonraki profili öğretmeye temiz bir listeyle başla.
            _objectSamples.Clear();
            RefreshClassList();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            ReleaseCamera();
        }

        // Görüntü İşleme sayfası aynı fiziksel kamerayı kullanmak isteyebilir (DirectShow
        // cihazları genelde tek seferde tek bağlantıya izin verir); sayfalar arası geçişte
        // shell bunu çağırarak donanım çakışmasını önler. Öğretilen örnekler korunur.
        public void ReleaseCamera()
        {
            _camera.Disconnect();
            pictureBoxPreview.Image?.Dispose();
            pictureBoxPreview.Image = null;
            UpdateConnectionUi();
        }

        private void SetupPropertySlider(TrackBar trackBar, Label valueLabel, CameraProperty property)
        {
            if (_camera.TryGetPropertyRange(property, out int min, out int max, out int defaultValue))
            {
                trackBar.Minimum = min;
                trackBar.Maximum = max;
                trackBar.Enabled = true;
                trackBar.Value = Math.Clamp(_camera.GetProperty(property), min, max);
            }
            else
            {
                trackBar.Enabled = false;
            }

            valueLabel.Text = trackBar.Enabled ? trackBar.Value.ToString() : "-";
        }

        private void trackBarBrightness_Scroll(object sender, EventArgs e) =>
            ApplySlider(trackBarBrightness, labelBrightnessValue, CameraProperty.Brightness);

        private void trackBarContrast_Scroll(object sender, EventArgs e) =>
            ApplySlider(trackBarContrast, labelContrastValue, CameraProperty.Contrast);

        private void trackBarSharpness_Scroll(object sender, EventArgs e) =>
            ApplySlider(trackBarSharpness, labelSharpnessValue, CameraProperty.Sharpness);

        private void trackBarSaturation_Scroll(object sender, EventArgs e) =>
            ApplySlider(trackBarSaturation, labelSaturationValue, CameraProperty.Saturation);

        private void ApplySlider(TrackBar trackBar, Label valueLabel, CameraProperty property)
        {
            _camera.SetProperty(property, trackBar.Value);
            valueLabel.Text = trackBar.Value.ToString();
        }

        private void buttonResetDefaults_Click(object sender, EventArgs e)
        {
            ResetPropertyToDefault(trackBarBrightness, labelBrightnessValue, CameraProperty.Brightness);
            ResetPropertyToDefault(trackBarContrast, labelContrastValue, CameraProperty.Contrast);
            ResetPropertyToDefault(trackBarSharpness, labelSharpnessValue, CameraProperty.Sharpness);
            ResetPropertyToDefault(trackBarSaturation, labelSaturationValue, CameraProperty.Saturation);
        }

        private void ResetPropertyToDefault(TrackBar trackBar, Label valueLabel, CameraProperty property)
        {
            if (!trackBar.Enabled) return;
            if (!_camera.TryGetPropertyRange(property, out _, out _, out int defaultValue)) return;

            trackBar.Value = Math.Clamp(defaultValue, trackBar.Minimum, trackBar.Maximum);
            _camera.SetProperty(property, trackBar.Value);
            valueLabel.Text = trackBar.Value.ToString();
        }

        private void Camera_FrameReceived(Bitmap frame)
        {
            if (InvokeRequired)
            {
                // BeginInvoke (bekletmeyen) kullanılıyor: Invoke kullanılsaydı, bu arka plan
                // thread'i UI thread'ini beklerken, UI thread'i de Bağlantıyı Kes sırasında
                // bu thread'in durmasını bekliyor (WaitForStop) — bu karşılıklı kilitlenmeye yol açar.
                if (IsDisposed) { frame.Dispose(); return; }
                try
                {
                    BeginInvoke(new Action<Bitmap>(Camera_FrameReceived), frame);
                }
                catch (ObjectDisposedException)
                {
                    frame.Dispose();
                }
                return;
            }

            _frameWidth = frame.Width;
            _frameHeight = frame.Height;

            if (_pendingSliderRestore)
            {
                _pendingSliderRestore = false;
                RestoreSliderValues();
            }

            if (!_roiInitialized)
            {
                _roiWidth = Math.Min((int)numericRoiWidth.Value, _frameWidth);
                _roiHeight = Math.Min((int)numericRoiHeight.Value, _frameHeight);
                _roiX = Math.Max(0, (_frameWidth - _roiWidth) / 2);
                _roiY = Math.Max(0, (_frameHeight - _roiHeight) / 2);
                _roiInitialized = true;
                UpdateRoiLabel();
            }

            // Nesne öğretme için, kutu çizilmemiş temiz bir ROI kırpması sakla.
            try
            {
                var roiRect = new Rectangle(_roiX, _roiY, _roiWidth, _roiHeight);
                UpdateLatestCrop(frame.Clone(roiRect, frame.PixelFormat));
            }
            catch { }

            using (var g = Graphics.FromImage(frame))
            using (var pen = new Pen(Color.FromArgb(0, 229, 255), 2))
            {
                g.DrawRectangle(pen, _roiX, _roiY, _roiWidth, _roiHeight);
            }

            var oldImage = pictureBoxPreview.Image;
            pictureBoxPreview.Image = frame;
            oldImage?.Dispose();
        }

        private void buttonRoiUp_Click(object sender, EventArgs e) => NudgeRoi(0, -RoiNudgeStep);
        private void buttonRoiDown_Click(object sender, EventArgs e) => NudgeRoi(0, RoiNudgeStep);
        private void buttonRoiLeft_Click(object sender, EventArgs e) => NudgeRoi(-RoiNudgeStep, 0);
        private void buttonRoiRight_Click(object sender, EventArgs e) => NudgeRoi(RoiNudgeStep, 0);

        private void NudgeRoi(int dx, int dy)
        {
            int maxX = Math.Max(0, _frameWidth - _roiWidth);
            int maxY = Math.Max(0, _frameHeight - _roiHeight);
            _roiX = Math.Clamp(_roiX + dx, 0, maxX);
            _roiY = Math.Clamp(_roiY + dy, 0, maxY);
            UpdateRoiLabel();
        }

        private void numericRoiWidth_ValueChanged(object sender, EventArgs e)
        {
            _roiWidth = (int)numericRoiWidth.Value;
            if (_frameWidth > 0)
            {
                _roiWidth = Math.Min(_roiWidth, _frameWidth);
                _roiX = Math.Clamp(_roiX, 0, Math.Max(0, _frameWidth - _roiWidth));
            }
            UpdateRoiLabel();
        }

        private void numericRoiHeight_ValueChanged(object sender, EventArgs e)
        {
            _roiHeight = (int)numericRoiHeight.Value;
            if (_frameHeight > 0)
            {
                _roiHeight = Math.Min(_roiHeight, _frameHeight);
                _roiY = Math.Clamp(_roiY, 0, Math.Max(0, _frameHeight - _roiHeight));
            }
            UpdateRoiLabel();
        }

        private void UpdateRoiLabel()
        {
            labelRoiPosition.Text = $"Konum: X={_roiX} Y={_roiY}";
        }

        // ---- Nesne öğretme ----

        private void buttonAddSample_Click(object sender, EventArgs e)
        {
            string className = textBoxClassName.Text.Trim();
            if (className.Length == 0)
            {
                MessageBox.Show("Önce bir nesne adı gir (örn. Elma).", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_recognitionService.IsModelLoaded)
            {
                MessageBox.Show("Tanıma modeli bulunamadı (Models\\mobilenetv2.onnx).", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var crop = CloneLatestCrop();
            if (crop == null)
            {
                MessageBox.Show("Önce kameraya bağlan ve canlı görüntü gelsin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var mat = BitmapConverter.ToMat(crop);
            var blobs = _recognitionService.DetectBlobs(mat);

            if (blobs.Count == 0)
            {
                MessageBox.Show("ROI alanında bir nesne bulunamadı. Nesneyi ROI dikdörtgeninin içine yerleştir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Öğretirken ROI içinde sadece tek nesne olduğu varsayılır; en büyük bölge alınır.
            var largest = blobs.OrderByDescending(c => Cv2.ContourArea(c)).First();
            using var blobMat = new Mat(mat, Cv2.BoundingRect(largest));
            var embedding = _recognitionService.ComputeEmbedding(blobMat);

            if (embedding == null)
            {
                MessageBox.Show("Öznitelik çıkarılamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _objectSamples.Add(new ObjectSample { ClassName = className, Embedding = embedding });
            RefreshClassList();
        }

        private void buttonDeleteClass_Click(object sender, EventArgs e)
        {
            if (listBoxClasses.SelectedItem == null)
            {
                MessageBox.Show("Silmek için bir nesne seç.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string entry = listBoxClasses.SelectedItem.ToString()!;
            string className = entry[..entry.LastIndexOf('(')].Trim();

            _objectSamples.RemoveAll(s => s.ClassName == className);
            RefreshClassList();
        }

        private void RefreshClassList()
        {
            listBoxClasses.Items.Clear();
            foreach (var group in _objectSamples.GroupBy(s => s.ClassName).OrderBy(g => g.Key))
                listBoxClasses.Items.Add($"{group.Key} ({group.Count()} örnek)");
        }

        private void UpdateLatestCrop(Bitmap crop)
        {
            lock (_cropLock)
            {
                _latestRoiCrop?.Dispose();
                _latestRoiCrop = crop;
            }
        }

        private Bitmap? CloneLatestCrop()
        {
            lock (_cropLock)
            {
                return _latestRoiCrop != null ? (Bitmap)_latestRoiCrop.Clone() : null;
            }
        }

        private void UpdateConnectionUi()
        {
            bool connected = _camera.IsConnected;

            labelStatus.Text = connected ? "Bağlı" : "Bağlı Değil";
            labelStatus.ForeColor = connected
                ? Color.FromArgb(76, 175, 80)
                : Color.FromArgb(239, 83, 80);

            buttonConnect.Enabled = !connected;
            buttonDisconnect.Enabled = connected;
            comboBoxCamera.Enabled = !connected;
            buttonResetDefaults.Enabled = connected;
            buttonSaveSettings.Enabled = connected;

            if (!connected)
            {
                foreach (var trackBar in new[] { trackBarBrightness, trackBarContrast, trackBarSharpness, trackBarSaturation })
                    trackBar.Enabled = false;

                foreach (var valueLabel in new[] { labelBrightnessValue, labelContrastValue, labelSharpnessValue, labelSaturationValue })
                    valueLabel.Text = "-";
            }
        }
    }
}
