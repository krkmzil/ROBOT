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
        // Henüz kaydedilmemiş örneklerin resimleri (küçük resim galerisi için); ObjectSample'a ait.
        private readonly Dictionary<ObjectSample, Bitmap> _sampleImages = new();
        private readonly DatasetService _datasetService = new();
        // Arka plan thread'i örnek listesini doğrudan okumasın diye anlık kopya kullanılır.
        private ObjectSample[] _samplesSnapshot = Array.Empty<ObjectSample>();
        private volatile List<Detection> _detections = new();
        private readonly DetectionTracker _tracker = new();
        private int _detectionBusy;
        private readonly ToolTip _toolTip = new();
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

            ClearSamples();
            _toolTip.Dispose();
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
            ErrorLog.Write($"Profil kaydediliyor: '{profileName}' ({_objectSamples.Count} nesne örneği)");

            // Örnek resimleri "Örnek Ekle"de veri setine zaten yazıldı; burada sadece profile bağlanır.
            int savedImageCount = _objectSamples.Count(s => s.ImageFile != null);

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
            ErrorLog.Write($"Profil dosyaya yazıldı: '{profileName}'");

            string message = savedImageCount > 0
                ? $"'{profileName}' profili kaydedildi. Bu profilin {savedImageCount} örnek resmi veri setinde."
                : $"'{profileName}' profili kaydedildi.";
            MessageBox.Show(message, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Bu profil kaydedildi; bir sonraki profili öğretmeye temiz bir listeyle başla.
            ClearSamples();
            ErrorLog.Write("Kaydetme işlemi tamamlandı");
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
            _tracker.Reset();
            _detections = new List<Detection>();
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

            StartDetectionIfIdle();

            using (var g = Graphics.FromImage(frame))
            using (var pen = new Pen(Color.FromArgb(0, 229, 255), 2))
            {
                g.DrawRectangle(pen, _roiX, _roiY, _roiWidth, _roiHeight);

                // ROI içindeki nesnelerin yeşil çerçevesi (şekil: listeden seçilen; tanınanlarda ad da yazar).
                DetectionOverlay.Draw(g, _detections, _roiX, _roiY);
            }

            var oldImage = pictureBoxPreview.Image;
            pictureBoxPreview.Image = frame;
            oldImage?.Dispose();
        }

        // Nesne bulma/tanıma ağır olduğu için arka planda çalışır; her karede değil, bir önceki
        // bitince yenisi başlar. Çizim her karede en son bulunan sonucu kullanır.
        private void StartDetectionIfIdle()
        {
            if (Interlocked.CompareExchange(ref _detectionBusy, 1, 0) != 0) return;

            var crop = CloneLatestCrop();
            if (crop == null)
            {
                Interlocked.Exchange(ref _detectionBusy, 0);
                return;
            }

            var samples = _samplesSnapshot;
            ObjectShape selectedShape = SelectedShape();

            Task.Run(() =>
            {
                try
                {
                    var raw = DetectionOverlay.Detect(_recognitionService, crop, samples, selectedShape, includeUnmatched: true);
                    _detections = _tracker.Update(raw);
                }
                catch
                {
                    _detections = new List<Detection>();
                }
                finally
                {
                    crop.Dispose();
                    Interlocked.Exchange(ref _detectionBusy, 0);
                }
            });
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

            var sample = new ObjectSample
            {
                ClassName = className,
                Embedding = embedding,
                Shape = SelectedShape()
            };

            var image = BitmapConverter.ToBitmap(blobMat);
            _objectSamples.Add(sample);
            _sampleImages[sample] = image;

            // Resim veri setinde hemen nesne adının klasörüne yazılır (profil kaydından bağımsız).
            try
            {
                sample.ImageFile = _datasetService.SaveSample(className, image);
                labelThumbCaption.Text = $"'{className}' klasöründe {_datasetService.CountImages(className)} resim var.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Örnek resmi veri setine kaydedilemedi: {ex.Message}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            RefreshClassList();
        }

        private ObjectShape SelectedShape() => comboBoxShape.SelectedIndex switch
        {
            1 => ObjectShape.Circle,
            2 => ObjectShape.Rectangle,
            _ => ObjectShape.Auto
        };

        private static string ShapeText(ObjectShape shape) => shape switch
        {
            ObjectShape.Circle => "Daire",
            ObjectShape.Rectangle => "Dikdörtgen",
            _ => "Otomatik"
        };

        private void buttonDeleteClass_Click(object sender, EventArgs e)
        {
            if (listBoxClasses.SelectedItem == null)
            {
                MessageBox.Show("Silmek için bir nesne seç.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string entry = listBoxClasses.SelectedItem.ToString()!;
            string className = entry[..entry.LastIndexOf('(')].Trim();

            foreach (var sample in _objectSamples.Where(s => s.ClassName == className).ToList())
                RemoveSampleCore(sample);

            RefreshClassList();
        }

        private void RemoveSampleCore(ObjectSample sample)
        {
            // Resim silinmeden önce küçük resim kutuları ondan ayrılmalı; yoksa kutu silinmiş resme
            // dokunur ve GDI+ "Parameter is not valid" hatası verir.
            ClearGalleryControls();

            _objectSamples.Remove(sample);
            if (_sampleImages.Remove(sample, out var image))
                image.Dispose();

            // Kullanıcı bu örneği sildiyse veri setindeki resmi de silinir.
            if (sample.ImageFile != null)
            {
                try { _datasetService.DeleteSample(sample.ImageFile); }
                catch { }
            }
        }

        private void ClearSamples()
        {
            // Önce küçük resim kutuları resimlerden ayrılır, sonra resimler silinir (sıra önemli).
            ClearGalleryControls();

            foreach (var image in _sampleImages.Values)
                image.Dispose();

            _sampleImages.Clear();
            _objectSamples.Clear();
            RefreshClassList();
        }

        private void RefreshClassList()
        {
            _samplesSnapshot = _objectSamples.ToArray();

            listBoxClasses.Items.Clear();
            foreach (var group in _objectSamples.GroupBy(s => s.ClassName).OrderBy(g => g.Key))
                listBoxClasses.Items.Add($"{group.Key} ({group.Count()} örnek)");

            RefreshSampleGallery();
        }

        // Küçük resim kutularını kaldırır. Resimlerin sahibi _sampleImages'tır, burada dispose edilmez;
        // kutudan önce ayrılır ki kutu silinirken resme dokunmasın.
        private void ClearGalleryControls()
        {
            flowLayoutPanelSamples.SuspendLayout();

            foreach (var control in flowLayoutPanelSamples.Controls.Cast<Control>().ToList())
            {
                if (control is PictureBox thumb)
                    thumb.Image = null;

                flowLayoutPanelSamples.Controls.Remove(control);
                control.Dispose();
            }

            flowLayoutPanelSamples.ResumeLayout();
        }

        // Eklenen her örnek için küçük bir resim gösterir; tıklayınca büyük görüntülenir.
        private void RefreshSampleGallery()
        {
            ClearGalleryControls();

            flowLayoutPanelSamples.SuspendLayout();

            foreach (var sample in _objectSamples)
            {
                if (!_sampleImages.TryGetValue(sample, out var image)) continue;

                var thumb = new PictureBox
                {
                    Size = new System.Drawing.Size(72, 72),
                    Margin = new Padding(3),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(18, 19, 28),
                    Cursor = Cursors.Hand,
                    Image = image,
                    Tag = sample
                };

                _toolTip.SetToolTip(thumb, $"{sample.ClassName} ({ShapeText(sample.Shape)}) - büyütmek için tıkla");
                thumb.Click += SampleThumb_Click;
                flowLayoutPanelSamples.Controls.Add(thumb);
            }

            flowLayoutPanelSamples.ResumeLayout();
        }

        private void SampleThumb_Click(object? sender, EventArgs e)
        {
            if (sender is not PictureBox thumb || thumb.Tag is not ObjectSample sample) return;
            if (!_sampleImages.TryGetValue(sample, out var image)) return;

            string details = $"Çerçeve: {ShapeText(sample.Shape)}   •   {image.Width}x{image.Height} piksel";

            bool deleteRequested;
            using (var preview = new FormSamplePreview(image, sample.ClassName, details))
            {
                preview.ShowDialog(FindForm());
                deleteRequested = preview.DeleteRequested;
            }

            if (deleteRequested)
            {
                RemoveSampleCore(sample);
                RefreshClassList();
            }
        }

        private void buttonOpenDataset_Click(object sender, EventArgs e)
        {
            try
            {
                _datasetService.OpenRootInExplorer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri seti klasörü açılamadı: {ex.Message}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
