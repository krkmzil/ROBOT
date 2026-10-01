using System.IO.Ports;

namespace ROBOT.Services
{
    public class SerialPortService
    {
        private SerialPort? _port;

        private readonly object _queueLock = new();
        private readonly Queue<string> _pendingCommands = new();
        private bool _waitingForOk = false;

        public bool IsConnected => _port != null && _port.IsOpen;

        // Bir satır veri geldiğinde tetiklenir (arka plan thread'inden gelir!)
        public event Action<string>? LineReceived;

        // Kuyruktaki bir komut gerçekten porta yazıldığı anda tetiklenir
        // (kuyruğa eklendiği anda değil) — böylece log gerçek gönderim sırasını yansıtır.
        public event Action<string>? LineSent;

        public string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        public bool Connect(string portName, int baudRate = 250000)
        {
            if (IsConnected) return true;

            try
            {
                _port = new SerialPort(portName, baudRate)
                {
                    NewLine = "\n",
                    ReadTimeout = 2000,
                    WriteTimeout = 2000
                };

                _port.DataReceived += Port_DataReceived;
                _port.Open();

                lock (_queueLock)
                {
                    _pendingCommands.Clear();
                    _waitingForOk = false;
                }

                return true;
            }
            catch
            {
                _port = null;
                return false;
            }
        }

        public void Disconnect()
        {
            if (_port == null) return;

            try
            {
                _port.DataReceived -= Port_DataReceived;
                if (_port.IsOpen) _port.Close();
            }
            catch { }
            finally
            {
                _port.Dispose();
                _port = null;
            }

            lock (_queueLock)
            {
                _pendingCommands.Clear();
                _waitingForOk = false;
            }
        }

        // Komutu kuyruğa ekler. Bir önceki komutun "ok" cevabı gelmeden
        // yenisi gönderilmez, böylece Marlin'e aynı anda tek komut gider.
        public void SendLine(string command)
        {
            if (!IsConnected) return;

            lock (_queueLock)
            {
                _pendingCommands.Enqueue(command);
            }

            TrySendNext();
        }

        private void TrySendNext()
        {
            string? next = null;

            lock (_queueLock)
            {
                if (_waitingForOk) return;
                if (_pendingCommands.Count == 0) return;

                next = _pendingCommands.Dequeue();
                _waitingForOk = true;
            }

            try
            {
                _port!.WriteLine(next);
                LineSent?.Invoke(next);
            }
            catch
            {
                lock (_queueLock) { _waitingForOk = false; }
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_port == null) return;

            try
            {
                string line = _port.ReadLine();
                LineReceived?.Invoke(line);

                string trimmed = line.Trim();
                bool isReply = trimmed.Equals("ok", StringComparison.OrdinalIgnoreCase) ||
                               trimmed.StartsWith("error", StringComparison.OrdinalIgnoreCase);

                if (isReply)
                {
                    lock (_queueLock) { _waitingForOk = false; }
                    TrySendNext();
                }
            }
            catch { }
        }
    }
}
