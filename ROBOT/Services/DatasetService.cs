using System.Diagnostics;
using System.Drawing.Imaging;

namespace ROBOT.Services
{
    // Öğretilen nesne resimlerini diskte "veri seti" olarak tutar: her nesne adı için tek bir klasör
    // vardır ve hangi profilde öğretilirse öğretilsin aynı isimli nesne aynı klasöre eklenir.
    //
    //   Belgelerim\ROBOT\Dataset\<nesne adı>\<tarih_saat>.png
    public class DatasetService
    {
        public string RootPath { get; } =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ROBOT", "Dataset");

        public string GetClassFolder(string className) =>
            Path.Combine(RootPath, SanitizeFolderName(className));

        // Resmi nesnenin klasörüne kaydeder; veri setine göre göreli yolu döner (profil kaydında saklanır).
        public string SaveSample(string className, Bitmap image)
        {
            string folder = GetClassFolder(className);
            Directory.CreateDirectory(folder);

            string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";
            string path = Path.Combine(folder, fileName);

            // Aynı milisaniyede birden fazla örnek eklenirse çakışmayı önle.
            int counter = 1;
            while (File.Exists(path))
            {
                fileName = $"{DateTime.Now:yyyyMMdd_HHmmss_fff}_{counter++}.png";
                path = Path.Combine(folder, fileName);
            }

            image.Save(path, ImageFormat.Png);
            return Path.Combine(Path.GetFileName(folder), fileName);
        }

        // SaveSample'ın döndürdüğü göreli yoldaki resmi siler (örnek öğretme sırasında silinirse).
        public void DeleteSample(string relativePath)
        {
            string path = Path.Combine(RootPath, relativePath);
            if (File.Exists(path))
                File.Delete(path);
        }

        public int CountImages(string className)
        {
            string folder = GetClassFolder(className);
            return Directory.Exists(folder) ? Directory.GetFiles(folder, "*.png").Length : 0;
        }

        // Veri seti klasörünü Windows Gezgini'nde açar (yoksa oluşturur).
        public void OpenRootInExplorer()
        {
            Directory.CreateDirectory(RootPath);
            Process.Start(new ProcessStartInfo("explorer.exe", $"\"{RootPath}\"") { UseShellExecute = true });
        }

        private static string SanitizeFolderName(string name)
        {
            string cleaned = name.Trim();
            foreach (char c in Path.GetInvalidFileNameChars())
                cleaned = cleaned.Replace(c, '_');

            cleaned = cleaned.TrimEnd('.', ' ');
            return cleaned.Length > 0 ? cleaned : "nesne";
        }
    }
}
