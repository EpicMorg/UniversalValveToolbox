using Microsoft.Win32;
using UniversalValveToolbox.Steam;
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

            // visual styles, default font, high DPI mode from the csproj (ApplicationVisualStyles / ApplicationHighDpiMode)
            ApplicationConfiguration.Initialize();

            LanguageManager.UpdateLanguage(currSettings.Language);

            const string line = "---------------------------------------------------------------------------";

            if (!SteamApi.Init(currSettings.ToolsAppId, out var error)) {
                DialogResult result = MessageBox.Show(@"Some error was occurred when trying to init Steam API:" + Environment.NewLine +
                    line + Environment.NewLine +
                    error + Environment.NewLine +
                    line + Environment.NewLine +
                    "Try to launch Steam?",
                    "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result != DialogResult.Yes) {
                    MessageBox.Show("Application will be terminated.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Application.Exit() before Run() does not stop Run(new FormMain())
                }

                try {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam")) {
                        ShellUtils.Start(key.GetValue("SteamExe").ToString());
                    }
                }
                catch (Exception exs) {
                    MessageBox.Show(@"Sorry. Steam launch failed because:" + Environment.NewLine +
                        line + Environment.NewLine +
                        exs.Message + Environment.NewLine +
                        line + Environment.NewLine +
                        "Application will be terminated.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Steam needs time to start and log in; the launcher is useless without it
                Cursor.Current = Cursors.WaitCursor;
                var started = SteamApi.WaitAndInit(currSettings.ToolsAppId, TimeSpan.FromSeconds(90), out error);
                Cursor.Current = Cursors.Default;
                if (!started) {
                    MessageBox.Show(@"Steam did not start in time:" + Environment.NewLine +
                        line + Environment.NewLine +
                        error + Environment.NewLine +
                        line + Environment.NewLine +
                        "Application will be terminated.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try {
                Application.Run(new FormMain());
            }
            finally {
                SteamApi.Shutdown();
            }
        }
    }
}
