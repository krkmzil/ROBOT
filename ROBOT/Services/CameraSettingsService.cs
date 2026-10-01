using System.Text.Json;

namespace ROBOT.Services
{
    public class CameraSettingsData
    {
        public string? CameraName { get; set; }
        public int? Brightness { get; set; }
        public int? Contrast { get; set; }
        public int? Sharpness { get; set; }
        public int? Saturation { get; set; }

        // İlgi alanı (ROI) — kamera görüntüsünün hangi dikdörtgen bölgesinin kullanılacağı.
        public int? RoiX { get; set; }
        public int? RoiY { get; set; }
        public int? RoiWidth { get; set; }
        public int? RoiHeight { get; set; }

        // Bu profil için öğretilen nesneler — Görüntü İşleme'de bu profil seçildiğinde
        // tanıma sadece bu listedeki nesnelerle sınırlı kalır.
        public List<ObjectSample>? ObjectSamples { get; set; }
    }

    // İsimli, birden fazla kayıtlı ayar profilini yönetir (örn. "Gündüz", "Gece", "Konveyor 1").
    public class CameraSettingsService
    {
        private readonly string _filePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "camera_profiles.json");

        private Dictionary<string, CameraSettingsData> LoadAll()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    var data = JsonSerializer.Deserialize<Dictionary<string, CameraSettingsData>>(json);
                    if (data != null) return data;
                }
            }
            catch { }

            return new Dictionary<string, CameraSettingsData>();
        }

        private void SaveAll(Dictionary<string, CameraSettingsData> profiles)
        {
            try
            {
                string json = JsonSerializer.Serialize(profiles, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch { }
        }

        public List<string> GetProfileNames()
        {
            return LoadAll().Keys.OrderBy(n => n).ToList();
        }

        public CameraSettingsData? LoadProfile(string name)
        {
            var profiles = LoadAll();
            return profiles.TryGetValue(name, out var data) ? data : null;
        }

        public void SaveProfile(string name, CameraSettingsData data)
        {
            var profiles = LoadAll();
            profiles[name] = data;
            SaveAll(profiles);
        }

        public void DeleteProfile(string name)
        {
            var profiles = LoadAll();
            if (profiles.Remove(name))
                SaveAll(profiles);
        }
    }
}
