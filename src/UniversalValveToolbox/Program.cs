using Steamworks;
using System;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var dataProvide = new DataProvider();
            var currSettings = dataProvide.Settings;

            LanguageManager.UpdateLanguage(currSettings.Language);

            SteamClient.Init(currSettings.ToolsAppId);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
