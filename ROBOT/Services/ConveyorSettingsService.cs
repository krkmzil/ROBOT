using System.Text.Json;

namespace ROBOT.Services
{
    public class ConveyorSettings
    {
        // Bandın bir tam motor turunda ilerlediği mesafe (mm) — ölçülerek girilir.
        public double MmPerRevolution { get; set; } = 100;
        public double SpeedMmPerSec { get; set; } = 20;
        public double DistanceMm { get; set; } = 50;

        // İleri/geri yönünü fiziksel kurulumla eşlemek için.
        public bool InvertDirection { get; set; }
    }

    // Konveyör ayarlarını conveyor_settings.json dosyasında saklar.
    public class ConveyorSettingsService
    {
        private readonly string _filePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conveyor_settings.json");

        public ConveyorSettings Load()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    var data = JsonSerializer.Deserialize<ConveyorSettings>(json);
                    if (data != null) return data;
                }
            }
            catch { }

            return new ConveyorSettings();
        }

        public void Save(ConveyorSettings settings)
        {
            try
            {
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch { }
        }
    }
}
