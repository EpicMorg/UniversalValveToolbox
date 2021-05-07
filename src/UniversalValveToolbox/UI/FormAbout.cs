using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UniversalValveToolbox {
    public partial class FormAbout : Form {
        public FormAbout() {
            InitializeComponent();

            #region move to helper
            System.Reflection.Assembly assemblyEpicMorgSteamPathsLib = System.Reflection.Assembly.LoadFrom("EpicMorg.SteamPathsLib.dll");
            System.Reflection.Assembly assemblyFacepunchSteamworksWin32 = System.Reflection.Assembly.LoadFrom("Facepunch.Steamworks.Win32.dll");
            System.Reflection.Assembly assemblykasthackbindingwf = System.Reflection.Assembly.LoadFrom("kasthack.binding.wf.dll");
            System.Reflection.Assembly assemblyGameloopVdf = System.Reflection.Assembly.LoadFrom("Gameloop.Vdf.dll");
            System.Reflection.Assembly assemblyNewtonsoftJson = System.Reflection.Assembly.LoadFrom("Newtonsoft.Json.dll");

            Version verEpicMorgSteamPathsLib = assemblyEpicMorgSteamPathsLib.GetName().Version;
            Version verFacepunchSteamworksWin32 = assemblyFacepunchSteamworksWin32.GetName().Version;
            Version verkasthackbindingwf = assemblykasthackbindingwf.GetName().Version;
            Version verGameloopVdf = assemblyGameloopVdf.GetName().Version;
            Version verNewtonsoftJson = assemblyNewtonsoftJson.GetName().Version;
            #endregion

            labelVersion.Text = Utils.VersionHelper.AssemblyVersion;
            labelTitle.Text = Utils.VersionHelper.AssemblyTitle;
            labelCopy.Text = Utils.VersionHelper.AssemblyCopyright;
            labelCaution.Text = labelTitle.Text + " " + labelCaution.Text;

            labelEpicMorgSteamPathsLibdll.Text = "EpicMorg.SteamPathsLib.dll " + verEpicMorgSteamPathsLib.ToString();
            labelFacepunchSteamworksWin32dll.Text = "Facepunch.Steamworks.Win32.dll " + verFacepunchSteamworksWin32.ToString();
            labelkasthackbindingwfdll.Text = "kasthack.binding.wf.dll " + verkasthackbindingwf.ToString();
            labelGameloopVdf.Text = "Gameloop.Vdf.dll " + verGameloopVdf.ToString();
            labelNewtonsoftJsondll.Text = "Newtonsoft.Json.dll " + verNewtonsoftJson.ToString();

        }

        private void FormAbout_Load(object sender, EventArgs e) {
            
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            Close();
        }

        private void linkLabelIconSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.flaticon.com/");
        }

        private void linkLabelFP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.flaticon.com/authors/freepik");
        }

        private void linkLabelTI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.flaticon.com/authors/those-icons");
        }

        private void linkLabelSI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.flaticon.com/authors/smashicons");
        }
    }
}
