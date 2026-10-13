using System.Text;

namespace ROBOT.Services
{
    // Beklenmeyen hataları ve önemli olayları (başlama, kapanma, kaydetme) bir günlük dosyasına yazar:
    //   Belgelerim\ROBOT\Logs\app.log
    // Uygulama kendiliğinden kapanırsa nedeni buradan okunabilir.
    public static class ErrorLog
    {
        private const long MaxFileBytes = 1_000_000;
        private static readonly object Lock = new();

        public static string FilePath { get; } =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ROBOT", "Logs", "app.log");

        public static void Write(string message)
        {
            try
            {
                lock (Lock)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

                    // Dosya şişmesin: büyüyünce eskiyi yedeğe alıp yeniden başla.
                    if (File.Exists(FilePath) && new FileInfo(FilePath).Length > MaxFileBytes)
                        File.Move(FilePath, FilePath + ".old", overwrite: true);

                    File.AppendAllText(FilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  {message}{Environment.NewLine}", Encoding.UTF8);
                }
            }
            catch
            {
                // Günlük yazılamıyorsa uygulamayı etkilememeli.
            }
        }

        public static void Write(Exception? exception, string source)
        {
            Write($"HATA ({source}): {exception}");
        }
    }
}
