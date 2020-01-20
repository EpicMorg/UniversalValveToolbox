using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalValveToolbox.Utils {
    static class LanguageManager {
        public static void UpdateLanguage(string newLanguage, bool restart = false) {
            var currThread = Thread.CurrentThread;
            var newCultureInfo = new CultureInfo(newLanguage);

            currThread.CurrentCulture = currThread.CurrentUICulture = newCultureInfo;

            if (restart) {
                Application.Restart();
            }
        }
    }
}
