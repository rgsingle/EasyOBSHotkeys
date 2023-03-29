namespace BetterInputMacros
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

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                // TODO: Logging
                MessageBox.Show("An unhandled exception has occurred, but I handled it.", "BIG OOPS");
            };

            Application.Run(new MainForm());
        }
    }
}