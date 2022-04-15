using Microsoft.Win32;
using Steamworks;
using System;
using System.Diagnostics;
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LanguageManager.UpdateLanguage(currSettings.Language);

            try { SteamClient.Init(currSettings.ToolsAppId); }
            catch (Exception ex) {

                DialogResult result = MessageBox.Show(@"Some error was occurred when trying to init Steam API:" + Environment.NewLine +
                    "---------------------------------------------------------------------------" + Environment.NewLine +
                    ex.Message + Environment.NewLine +
                    "---------------------------------------------------------------------------" + Environment.NewLine +
                    "Try to launch Steam?", 
                    "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    try {
                        string steamPATH = ""; 
                        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam");
                        steamPATH = key.GetValue("SteamExe").ToString();
                        Process.Start(steamPATH);
                        key.Close();
                    }
                    catch (Exception exs) {
                        MessageBox.Show(@"Sorry. Steam launch failed because:" + Environment.NewLine +
                        "---------------------------------------------------------------------------" + Environment.NewLine +
                        exs.Message + Environment.NewLine +
                        "---------------------------------------------------------------------------" + Environment.NewLine +
                        "Application will be terminated.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show("Application will be terminated.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }
               
            }

            Application.Run(new FormMain());
        }
    }
}
