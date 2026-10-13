using System.Text.RegularExpressions;
using ROBOT.Services;

namespace ROBOT
{
    public partial class MotorControlPanel : UserControl
    {
        // Seri bağlantı ve B/A servisi tüm sayfalarla paylaşılır (konveyör sayfası da aynısını kullanır).
        private readonly SerialPortService _serial;
        private readonly ExtraAxesService _axes;
        private readonly System.Windows.Forms.Timer _bootTimer = new() { Interval = 2500 };
        private bool _boardReady = false;
        private double _jogStep = 1.0;

        private static readonly Color StepButtonActiveColor = Color.FromArgb(91, 110, 245);
        private static readonly Color StepButtonInactiveColor = Color.FromArgb(38, 42, 61);

        private static readonly Regex PositionRegex = new(
            @"X:(-?\d+\.?\d*)\s+Y:(-?\d+\.?\d*)\s+Z:(-?\d+\.?\d*)",
            RegexOptions.Compiled);

        public MotorControlPanel(SerialPortService serial, ExtraAxesService axes)
        {
            _serial = serial;
            _axes = axes;

            InitializeComponent();
            _serial.LineReceived += Serial_LineReceived;
            _serial.LineSent += Serial_LineSent;
            _axes.BPositionChanged += UpdateBDro;

            buttonStep01.Tag = 0.1;
            buttonStep1.Tag = 1.0;
            buttonStep10.Tag = 10.0;

            buttonJogXPlus.Tag = ("X", 1);
            buttonJogXMinus.Tag = ("X", -1);
            buttonJogYPlus.Tag = ("Y", 1);
            buttonJogYMinus.Tag = ("Y", -1);
            buttonJogZPlus.Tag = ("Z", 1);
            buttonJogZMinus.Tag = ("Z", -1);
            buttonJogBPlus.Tag = ("B", 1);
            buttonJogBMinus.Tag = ("B", -1);

            textBoxCustomStep.TextChanged += (_, __) => UpdateStepCenterDisplay();

            comboBoxPort.DropDown += (_, __) => RefreshPortList();

            _bootTimer.Tick += (_, __) =>
            {
                _bootTimer.Stop();
                _boardReady = true;
                AppendLog("Kart hazır, komut gönderilebilir.");

                _axes.Initialize();
                _axes.BoardReady = true;
            };
        }

        private void UpdateBDro()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateBDro));
                return;
            }

            labelDroBValue.Text = _axes.BPosition.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture);
        }

        private void MotorControlPanel_Load(object sender, EventArgs e)
        {
            RefreshPortList();
        }

        // Sekme değişince veya uygulama kapanınca çağrılır; bağlantıyı güvenle kapatır.
        public void Cleanup()
        {
            _bootTimer.Stop();
            _axes.BoardReady = false;
            _serial.Disconnect();
        }

        // Bağlıyken ve kart açılış süresini tamamlamışken true döner; değilse
        // kullanıcıyı uyarıp false döner. Tüm komut gönderen butonlar bunu kullanır.
        private bool CheckReady()
        {
            if (!_serial.IsConnected)
            {
                MessageBox.Show("Robot bağlı değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!_boardReady)
            {
                MessageBox.Show("Kart henüz açılıyor, birkaç saniye bekle.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Hem ilk yüklemede hem de dropdown her açıldığında çağrılır, böylece
        // uygulama açıkken sonradan takılan USB-seri cihazlar da listeye düşer.
        private void RefreshPortList()
        {
            string? previouslySelected = comboBoxPort.SelectedItem?.ToString();

            comboBoxPort.Items.Clear();
            comboBoxPort.Items.AddRange(_serial.GetAvailablePorts());

            if (previouslySelected != null && comboBoxPort.Items.Contains(previouslySelected))
                comboBoxPort.SelectedItem = previouslySelected;
            else if (comboBoxPort.Items.Count > 0)
                comboBoxPort.SelectedIndex = 0;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (comboBoxPort.SelectedItem == null)
            {
                MessageBox.Show("Önce bir COM port seç.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string portName = comboBoxPort.SelectedItem.ToString()!;
            bool ok = _serial.Connect(portName, 250000); // Marlin bu kartta 250000 baud kullanıyor

            if (!ok)
            {
                MessageBox.Show("Bağlanamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateConnectionUi();
            AppendLog($"[{portName}] bağlandı.");

            _boardReady = false;
            AppendLog("Kart açılıyor, lütfen bekleyin...");
            _bootTimer.Start();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            _bootTimer.Stop();
            _boardReady = false;
            _axes.BoardReady = false;

            // Bağlantı kesilince kart hareketi sürdürürdü; bant dönüyorsa önce durdur.
            if (_axes.IsARunning)
                _axes.StopA();

            _serial.Disconnect();
            UpdateConnectionUi();
            labelAlarm.Text = "";
            ResetDro();
            _axes.ResetB();
            AppendLog("Bağlantı kesildi.");
        }

        private void buttonZeroX_Click(object sender, EventArgs e) => ZeroAxis("X");
        private void buttonZeroY_Click(object sender, EventArgs e) => ZeroAxis("Y");
        private void buttonZeroZ_Click(object sender, EventArgs e) => ZeroAxis("Z");
        private void buttonZeroB_Click(object sender, EventArgs e) => _axes.ZeroB();

        private void ZeroAxis(string axis)
        {
            if (!CheckReady()) return;

            _serial.SendLine($"G92 {axis}0");
            _serial.SendLine("M114");
        }

        private void ResetDro()
        {
            labelDroXValue.Text = "0.000";
            labelDroYValue.Text = "0.000";
            labelDroZValue.Text = "0.000";
            labelDroBValue.Text = "0.000";
        }

        private void buttonStep_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            _jogStep = (double)button.Tag!;
            textBoxCustomStep.Text = "";

            UpdateStepCenterDisplay();
        }

        private void UpdateStepCenterDisplay()
        {
            string customText = textBoxCustomStep.Text.Trim();
            bool hasCustom = customText.Length > 0;

            if (hasCustom)
            {
                labelJogStepCenter.Text = customText + "mm";
                foreach (var btn in new[] { buttonStep01, buttonStep1, buttonStep10 })
                    btn.BackColor = StepButtonInactiveColor;
                return;
            }

            labelJogStepCenter.Text = FormatStep(_jogStep) + "mm";

            foreach (var btn in new[] { buttonStep01, buttonStep1, buttonStep10 })
                btn.BackColor = (double)btn.Tag! == _jogStep ? StepButtonActiveColor : StepButtonInactiveColor;
        }

        private void buttonJog_Click(object sender, EventArgs e)
        {
            if (!CheckReady()) return;

            var (axis, sign) = ((string, int))((Button)sender).Tag!;
            double step = _jogStep;

            string customText = textBoxCustomStep.Text.Trim();
            if (customText.Length > 0)
            {
                if (!double.TryParse(customText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out step) || step <= 0)
                {
                    MessageBox.Show("Özel mesafe geçerli bir pozitif sayı olmalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            double distance = step * sign;

            if (axis == "B")
            {
                _axes.MoveB(distance);
                return;
            }

            int feed = axis == "Z" ? 500 : 1500;
            string distanceStr = distance.ToString("0.###", System.Globalization.CultureInfo.InvariantCulture);

            _serial.SendLine("G91");
            _serial.SendLine($"G1 {axis}{distanceStr} F{feed}");
            _serial.SendLine("G90");
            _serial.SendLine("M114");
        }

        private void buttonBFullTurn_Click(object sender, EventArgs e)
        {
            if (!CheckReady()) return;

            _axes.MoveB(360);
        }

        private static string FormatStep(double step) =>
            step.ToString("0.###", System.Globalization.CultureInfo.InvariantCulture);

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendCurrentCommand();
        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            if (!CheckReady()) return;

            _serial.SendLine("M114"); // Marlin: mevcut pozisyonu raporla
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            if (!CheckReady()) return;

            // Home sırası: Z, X, Y (Marlin'in kendi sırası X, Y, Z'dir), sonra B, sonra konveyör.
            // Her G28 bitince "ok" döndüğü için sıra garantidir.
            _serial.SendLine("G28 Z");
            _serial.SendLine("G28 X");
            _serial.SendLine("G28 Y");
            _serial.SendLine("M114");

            // B ve konveyörün uç anahtarı yok: B sıfır konumuna döner, konveyör durdurulur.
            if (Math.Abs(_axes.BPosition) > 0.001)
                _axes.MoveB(-_axes.BPosition);

            _axes.StopA();
        }

        private void buttonSendBatch_Click(object sender, EventArgs e)
        {
            if (!CheckReady()) return;

            var lines = textBoxBatch.Text
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToList();

            if (lines.Count == 0)
            {
                MessageBox.Show("Gönderilecek satır yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var line in lines)
            {
                _serial.SendLine(line);
            }

            textBoxBatch.Clear();
        }

        private void textBoxGcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendCurrentCommand();
            }
        }

        private void SendCurrentCommand()
        {
            string command = textBoxGcode.Text.Trim();
            if (string.IsNullOrEmpty(command)) return;

            if (!CheckReady()) return;

            _serial.SendLine(command);
            textBoxGcode.Clear();
        }

        private void Serial_LineReceived(string line)
        {
            // Bu event arka plan thread'inden geliyor, UI'a dokunmak için Invoke şart.
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Serial_LineReceived), line);
                return;
            }

            AppendLog($"< {line}");
            UpdateAlarmIndicator(line);
            UpdateDroFromLine(line);
        }

        private void UpdateDroFromLine(string line)
        {
            var match = PositionRegex.Match(line);
            if (!match.Success) return;

            labelDroXValue.Text = FormatDro(match.Groups[1].Value);
            labelDroYValue.Text = FormatDro(match.Groups[2].Value);
            labelDroZValue.Text = FormatDro(match.Groups[3].Value);
        }

        private static string FormatDro(string rawValue)
        {
            return double.TryParse(rawValue, System.Globalization.CultureInfo.InvariantCulture, out double v)
                ? v.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture)
                : rawValue;
        }

        private void UpdateAlarmIndicator(string line)
        {
            string trimmed = line.Trim();

            // Marlin hata/durma bildirimleri "Error:" ile başlar (örn. "Error:Printer halted. kill() called!")
            bool isAlarm = trimmed.StartsWith("Error:", StringComparison.OrdinalIgnoreCase);

            if (isAlarm)
                labelAlarm.Text = "⚠ ALARM";
            else if (trimmed.Equals("ok", StringComparison.OrdinalIgnoreCase))
                labelAlarm.Text = "";
        }

        private void Serial_LineSent(string line)
        {
            // Bu event arka plan thread'inden geliyor, UI'a dokunmak için Invoke şart.
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Serial_LineSent), line);
                return;
            }

            AppendLog($"> {line}");
        }

        private void UpdateConnectionUi()
        {
            bool connected = _serial.IsConnected;

            labelStatus.Text = connected ? "Bağlı" : "Bağlı Değil";
            labelStatus.ForeColor = connected
                ? Color.FromArgb(76, 175, 80)
                : Color.FromArgb(239, 83, 80);

            buttonConnect.Enabled = !connected;
            buttonDisconnect.Enabled = connected;
            comboBoxPort.Enabled = !connected;
        }

        private void AppendLog(string text)
        {
            textBoxLog.AppendText(text + Environment.NewLine);
        }
    }
}
