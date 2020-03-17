namespace UniversalValveToolbox.Utils
{
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    internal static class LanguageManager
    {
        public static void UpdateLanguage(string newLanguage, bool restart = false)
        {
            var currThread = Thread.CurrentThread;
            var newCultureInfo = new CultureInfo(newLanguage);

            currThread.CurrentCulture = currThread.CurrentUICulture = newCultureInfo;

            if (restart)
            {
                Application.Restart();
            }
        }
    }
}
