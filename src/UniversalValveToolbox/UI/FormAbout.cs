using System;
using System.Windows.Forms;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox {
    public partial class FormAbout : Form {
        public FormAbout() {
            InitializeComponent();

            labelVersion.Text = Utils.VersionHelper.AssemblyVersion;
            labelTitle.Text = Utils.VersionHelper.AssemblyTitle;
            labelCopy.Text = Utils.VersionHelper.AssemblyCopyright;
            labelCaution.Text = labelTitle.Text + " " + labelCaution.Text;

            // versions of the referenced libraries (already loaded; file names differ per platform, e.g. Facepunch.Steamworks.Win64)
            labelFacepunchSteamworksWin32dll.Text = Describe(typeof(Steamworks.SteamClient).Assembly);
            labelkasthackbindingwfdll.Text = Describe(System.Reflection.Assembly.Load("kasthack.binding.wf"));
            labelNewtonsoftJsondll.Text = Describe(typeof(Newtonsoft.Json.JsonConvert).Assembly);
        }

        private static string Describe(System.Reflection.Assembly assembly) {
            var name = assembly.GetName();
            return $"{name.Name}.dll {name.Version}";

        }

        private void FormAbout_Load(object sender, EventArgs e) {
            
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            Close();
        }

        private void linkLabelIconSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ShellUtils.Start("https://www.flaticon.com/");
        }

        private void linkLabelFP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ShellUtils.Start("https://www.flaticon.com/authors/freepik");
        }

        private void linkLabelTI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ShellUtils.Start("https://www.flaticon.com/authors/those-icons");
        }

        private void linkLabelSI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShellUtils.Start("https://www.flaticon.com/authors/smashicons");
        }
    }
}
