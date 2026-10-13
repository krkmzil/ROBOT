using System.Globalization;

namespace ROBOT.Services
{
    // B (E0 sürücüsü, dönme) ve A (E1 sürücüsü, konveyör) motorlarını yönetir. İkisi de derece
    // cinsindendir (200 adım x 1/16 mikroadım = 3200 adım/tur).
    //
    // B, Marlin'in normal E ekseni olarak sürülür (G1 E..), X/Y/Z ile aynı komutta birlikte de
    // hareket edebilir. A (konveyör) ise Marlin'in hareket planlayıcısından bağımsızdır: firmware
    // içinde kendi zamanlayıcısıyla adım üretir (M800/M801), bu yüzden dönerken X/Y/Z/B aynı
    // anda hareket ettirilebilir.
    public class ExtraAxesService
    {
        public const double StepsPerDegree = 3200.0 / 360.0;
        public const int BFeed = 3600; // derece/dk

        private const int MaxRateStepsPerSec = 8000; // firmware'deki MAX_RATE ile aynı

        private readonly SerialPortService _serial;
        private bool _continuous;
        private DateTime _moveEndsAtUtc = DateTime.MinValue;

        public ExtraAxesService(SerialPortService serial)
        {
            _serial = serial;
        }

        // Kart açılıp komut alabilir hale gelince Motor Kontrol sayfası tarafından işaretlenir.
        public bool BoardReady { get; set; }

        public bool IsConnected => _serial.IsConnected;

        // B ekseninin komut edilen konumu (derece).
        public double BPosition { get; private set; }

        public event Action? BPositionChanged;

        // Konveyör sürekli çalıştırıldı ya da belirli mesafe hareketi hâlâ sürüyor olabilir.
        public bool IsARunning => _continuous || DateTime.UtcNow < _moveEndsAtUtc;

        // Kart hazır olunca bir kez çağrılır: E (B ekseni) adım/birim değerini derece olarak ayarlar.
        public void Initialize()
        {
            string steps = StepsPerDegree.ToString("0.####", CultureInfo.InvariantCulture);
            _serial.SendLine($"M92 E{steps}");

            ResetB();
        }

        public void ResetB()
        {
            BPosition = 0;
            BPositionChanged?.Invoke();
        }

        // ---- B ekseni (dönme) ----

        public void MoveB(double degrees, int feed = BFeed)
        {
            string distance = degrees.ToString("0.###", CultureInfo.InvariantCulture);

            _serial.SendLine("G91");
            _serial.SendLine($"G1 E{distance} F{feed}");
            _serial.SendLine("G90");

            BPosition += degrees;
            BPositionChanged?.Invoke();
        }

        public void ZeroB() => ResetB();

        // ---- A ekseni (konveyör) ----
        // feed: motor mili hızı, derece/dk.

        // Motor milini belirli bir açı kadar döndürür; karttaki adım sayacı tam o kadar adımda durdurur.
        public void MoveA(double degrees, int feed)
        {
            int rate = RateFromFeed(feed);
            long steps = (long)Math.Round(Math.Abs(degrees) * StepsPerDegree);
            if (steps <= 0) return;

            bool forward = degrees > 0;
            _serial.SendLine($"M800 S{rate} D{(forward ? 1 : 0)} P{steps}");

            _continuous = false;
            double seconds = steps / (double)rate + 1.0; // hızlanma/yavaşlama payı
            _moveEndsAtUtc = DateTime.UtcNow.AddSeconds(seconds);
        }

        // Durdurulana kadar sürekli döndürür.
        public void StartA(bool forward, int feed)
        {
            _serial.SendLine($"M800 S{RateFromFeed(feed)} D{(forward ? 1 : 0)}");

            _continuous = true;
            _moveEndsAtUtc = DateTime.MinValue;
        }

        // Konveyörü anında durdurur (kuyruktaki diğer komutları beklemeden).
        public void StopA()
        {
            if (_serial.IsConnected)
                _serial.SendLine("M801");

            _continuous = false;
            _moveEndsAtUtc = DateTime.MinValue;
        }

        private static int RateFromFeed(int feedDegPerMin)
        {
            double stepsPerSec = feedDegPerMin / 60.0 * StepsPerDegree;
            return Math.Clamp((int)Math.Round(stepsPerSec), 1, MaxRateStepsPerSec);
        }
    }
}
