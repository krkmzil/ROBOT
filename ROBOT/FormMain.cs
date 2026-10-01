namespace ROBOT
{
    public partial class FormMain : Form
    {
        private readonly MotorControlPanel _motorPanel = new();
        private readonly CameraSettingsPanel _cameraPanel = new();
        private readonly ImageProcessingPanel _imagePanel = new();

        private static readonly Color NavActiveColor = Color.FromArgb(91, 110, 245);
        private static readonly Color NavInactiveColor = Color.FromArgb(35, 37, 53);

        private UserControl? _activePanel;

        public FormMain()
        {
            InitializeComponent();
            FormClosing += FormMain_FormClosing;

            ShowPanel(_motorPanel, buttonNavMotor);
        }

        private void buttonNavMotor_Click(object sender, EventArgs e) => ShowPanel(_motorPanel, buttonNavMotor);
        private void buttonNavCamera_Click(object sender, EventArgs e) => ShowPanel(_cameraPanel, buttonNavCamera);
        private void buttonNavImage_Click(object sender, EventArgs e) => ShowPanel(_imagePanel, buttonNavImage);

        // Sayfalar arasında geçiş yaparken hiçbirini kapatmaz; motor bağlantısı gibi
        // paylaşılmayan kaynaklar arka planda korunur. Ama Kamera Ayarları ve Görüntü
        // İşleme aynı fiziksel kamerayı kullanabildiği için (çoğu kamera aynı anda tek
        // bağlantıya izin verir), sayfadan ayrılırken o sayfanın kamerası bırakılır —
        // aksi halde diğer sayfa kamerayı açamaz ve ekran siyah kalır.
        private void ShowPanel(UserControl panel, Button navButton)
        {
            if (_activePanel == panel) return;

            if (_activePanel is CameraSettingsPanel leavingCameraSettings)
                leavingCameraSettings.ReleaseCamera();
            else if (_activePanel is ImageProcessingPanel leavingImageProcessing)
                leavingImageProcessing.ReleaseCamera();

            panelContent.SuspendLayout();
            panelContent.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            panelContent.Controls.Add(panel);
            panelContent.ResumeLayout();

            foreach (var btn in new[] { buttonNavMotor, buttonNavCamera, buttonNavImage })
                btn.BackColor = NavInactiveColor;

            navButton.BackColor = NavActiveColor;
            _activePanel = panel;
        }

        private void FormMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _motorPanel.Cleanup();
            _cameraPanel.Cleanup();
            _imagePanel.Cleanup();
        }
    }
}
