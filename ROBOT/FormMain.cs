using ROBOT.Services;

namespace ROBOT
{
    public partial class FormMain : Form
    {
        // Seri bağlantı tüm sayfalar arasında paylaşılır: bağlantıyı Motor Kontrol açar,
        // diğer sayfalar (ör. konveyör) aynı bağlantı üzerinden komut gönderir.
        private readonly SerialPortService _serial = new();
        private readonly ExtraAxesService _axes;

        private readonly MotorControlPanel _motorPanel;
        private readonly CameraSettingsPanel _cameraPanel = new();
        private readonly ConveyorSettingsPanel _conveyorPanel;
        private readonly ImageProcessingPanel _imagePanel = new();

        private static readonly Color NavActiveColor = Color.FromArgb(91, 110, 245);
        private static readonly Color NavInactiveColor = Color.FromArgb(35, 37, 53);

        private UserControl? _activePanel;

        public FormMain()
        {
            _axes = new ExtraAxesService(_serial);
            _motorPanel = new MotorControlPanel(_serial, _axes);
            _conveyorPanel = new ConveyorSettingsPanel(_axes);

            InitializeComponent();
            FormClosing += FormMain_FormClosing;

            ShowPanel(_motorPanel, buttonNavMotor);
        }

        private void buttonNavMotor_Click(object sender, EventArgs e) => ShowPanel(_motorPanel, buttonNavMotor);
        private void buttonNavCamera_Click(object sender, EventArgs e) => ShowPanel(_cameraPanel, buttonNavCamera);
        private void buttonNavConveyor_Click(object sender, EventArgs e) => ShowPanel(_conveyorPanel, buttonNavConveyor);
        private void buttonNavImage_Click(object sender, EventArgs e) => ShowPanel(_imagePanel, buttonNavImage);

        // Sayfalar arasında geçiş yaparken hiçbirini kapatmaz; motor bağlantısı gibi
        // paylaşılmayan kaynaklar arka planda korunur. Ama Kamera Ayarları ve Görüntü
        // İşleme aynı fiziksel kamerayı kullanabildiği için (çoğu kamera aynı anda tek
        // bağlantıya izin verir), sayfadan ayrılırken o sayfanın kamerası bırakılır —
        // aksi halde diğer sayfa kamerayı açamaz ve ekran siyah kalır.
        private void ShowPanel(UserControl panel, Button navButton)
        {
            if (_activePanel == panel) return;

            // Ayrılan sayfa kamerayı bırakır ama ayarlarını hatırlar; geri dönüldüğünde
            // sayfa kendini otomatik olarak son kaldığı haliyle yeniden bağlar.
            if (_activePanel is CameraSettingsPanel leavingCameraSettings)
                leavingCameraSettings.SuspendCamera();
            else if (_activePanel is ImageProcessingPanel leavingImageProcessing)
                leavingImageProcessing.SuspendCamera();

            panelContent.SuspendLayout();
            panelContent.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            panelContent.Controls.Add(panel);
            panelContent.ResumeLayout();

            foreach (var btn in new[] { buttonNavMotor, buttonNavCamera, buttonNavConveyor, buttonNavImage })
                btn.BackColor = NavInactiveColor;

            navButton.BackColor = NavActiveColor;
            _activePanel = panel;

            if (panel is CameraSettingsPanel enteringCameraSettings)
                enteringCameraSettings.ResumeCamera();
            else if (panel is ImageProcessingPanel enteringImageProcessing)
                enteringImageProcessing.ResumeCamera();
        }

        private void FormMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            ErrorLog.Write($"Ana pencere kapaniyor, neden: {e.CloseReason}");

            // Konveyör önce durdurulmalı, çünkü bağlantı kapanınca kart hareketi sürdürürdü.
            _conveyorPanel.Cleanup();
            _motorPanel.Cleanup();
            _cameraPanel.Cleanup();
            _imagePanel.Cleanup();
        }
    }
}
