using ROBOT.Services;

namespace ROBOT
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            ErrorLog.Write("Uygulama basladi");
            RegisterErrorHandlers();

            Application.Run(new FormMain());
        }

        // Beklenmeyen hatalar günlüğe yazılır; arayüz thread'indeki hatalarda uygulama kapanmak
        // yerine hatayı gösterip çalışmaya devam eder.
        private static void RegisterErrorHandlers()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += (_, e) =>
            {
                ErrorLog.Write(e.Exception, "arayuz thread'i");
                MessageBox.Show(
                    $"Beklenmeyen bir hata oluştu:\n\n{e.Exception.Message}\n\nAyrıntı günlük dosyasında: {ErrorLog.FilePath}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                ErrorLog.Write(e.ExceptionObject as Exception, $"islenmeyen hata, uygulama kapaniyor={e.IsTerminating}");
            };

            TaskScheduler.UnobservedTaskException += (_, e) =>
            {
                ErrorLog.Write(e.Exception, "arka plan gorevi");
                e.SetObserved();
            };

            AppDomain.CurrentDomain.ProcessExit += (_, _) => ErrorLog.Write("Uygulama sonlandi");
        }
    }
}
