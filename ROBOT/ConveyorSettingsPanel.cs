using ROBOT.Services;

namespace ROBOT
{
    public partial class ConveyorSettingsPanel : UserControl
    {
        // Firmware'deki E hız tavanı (derece/sn) — DEFAULT_MAX_FEEDRATE'in E değeriyle aynı olmalı.
        private const double MaxMotorDegPerSec = 720;

        private readonly ExtraAxesService _axes;
        private readonly ConveyorSettingsService _settingsService = new();
        private readonly System.Windows.Forms.Timer _statusTimer = new() { Interval = 500 };
        private bool _loading = true;

        public ConveyorSettingsPanel(ExtraAxesService axes)
        {
            _axes = axes;

            InitializeComponent();
            LoadSettings();
            UpdateSpeedLimit();
            _loading = false;

            _statusTimer.Tick += (_, __) => UpdateStatus();
            _statusTimer.Start();
            UpdateStatus();
        }

        // Uygulama kapanırken çağrılır: bant dönüyorsa durdurur (FormMain bunu seri bağlantı
        // kapanmadan önce çağırır, yoksa kart hareketi sürdürürdü).
        public void Cleanup()
        {
            _statusTimer.Stop();

            if (_axes.IsARunning)
                _axes.StopA();
        }

        private void LoadSettings()
        {
            var settings = _settingsService.Load();

            numericMmPerRev.Value = Clamp(settings.MmPerRevolution, numericMmPerRev);
            numericSpeed.Value = Clamp(settings.SpeedMmPerSec, numericSpeed);
            numericDistance.Value = Clamp(settings.DistanceMm, numericDistance);
            checkBoxInvert.Checked = settings.InvertDirection;
        }

        private static decimal Clamp(double value, NumericUpDown numeric) =>
            Math.Clamp((decimal)value, numeric.Minimum, numeric.Maximum);

        private void SaveSettings()
        {
            if (_loading) return;

            _settingsService.Save(new ConveyorSettings
            {
                MmPerRevolution = (double)numericMmPerRev.Value,
                SpeedMmPerSec = (double)numericSpeed.Value,
                DistanceMm = (double)numericDistance.Value,
                InvertDirection = checkBoxInvert.Checked
            });
        }

        private void numericMmPerRev_ValueChanged(object sender, EventArgs e)
        {
            UpdateSpeedLimit();
            SaveSettings();
        }

        private void settingChanged(object sender, EventArgs e) => SaveSettings();

        // Motorun hız tavanı mm/sn cinsinden bandın tur mesafesine bağlıdır.
        private void UpdateSpeedLimit()
        {
            decimal maxSpeed = Math.Max(1, Math.Round((decimal)(MaxMotorDegPerSec / 360.0) * numericMmPerRev.Value, 1));
            numericSpeed.Maximum = maxSpeed;
            labelSpeedMax.Text = $"En fazla: {maxSpeed:0.#} mm/sn";
        }

        private double DegreesPerMm => 360.0 / (double)numericMmPerRev.Value;

        private int CurrentFeed() =>
            Math.Max(1, (int)Math.Round((double)numericSpeed.Value * DegreesPerMm * 60)); // derece/dk

        // forward: kullanıcının "ileri" dediği yön; ters çevir seçiliyse motor yönü tersine döner.
        private bool MotorPositive(bool forward) => forward ^ checkBoxInvert.Checked;

        private bool CheckReady()
        {
            if (!_axes.IsConnected || !_axes.BoardReady)
            {
                MessageBox.Show("Önce Motor Kontrol sayfasından robota bağlan ve kartın hazır olmasını bekle.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void buttonStartForward_Click(object sender, EventArgs e) => StartContinuous(forward: true);
        private void buttonStartBackward_Click(object sender, EventArgs e) => StartContinuous(forward: false);

        private void StartContinuous(bool forward)
        {
            if (!CheckReady()) return;

            _axes.StartA(MotorPositive(forward), CurrentFeed());
            UpdateStatus();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _axes.StopA();
            UpdateStatus();
        }

        private void buttonOneTurn_Click(object sender, EventArgs e)
        {
            if (!CheckReady()) return;

            _axes.MoveA(MotorPositive(true) ? 360 : -360, CurrentFeed());
        }

        private void buttonMoveForward_Click(object sender, EventArgs e) => MoveDistance(forward: true);
        private void buttonMoveBackward_Click(object sender, EventArgs e) => MoveDistance(forward: false);

        private void MoveDistance(bool forward)
        {
            if (!CheckReady()) return;

            double degrees = (double)numericDistance.Value * DegreesPerMm;
            _axes.MoveA(MotorPositive(forward) ? degrees : -degrees, CurrentFeed());
        }

        private void UpdateStatus()
        {
            if (!_axes.IsConnected)
            {
                labelStatus.Text = "Robot bağlı değil (Motor Kontrol sayfasından bağlan)";
                labelStatus.ForeColor = Color.FromArgb(239, 83, 80);
            }
            else if (!_axes.BoardReady)
            {
                labelStatus.Text = "Kart açılıyor, birkaç saniye bekle...";
                labelStatus.ForeColor = Color.FromArgb(255, 167, 38);
            }
            else
            {
                labelStatus.Text = "Hazır";
                labelStatus.ForeColor = Color.FromArgb(76, 175, 80);
            }

            bool running = _axes.IsConnected && _axes.IsARunning;
            labelRunState.Text = running ? "Konveyör çalışıyor" : "Konveyör durdu";
            labelRunState.ForeColor = running ? Color.FromArgb(76, 175, 80) : Color.FromArgb(174, 180, 199);
        }
    }
}
