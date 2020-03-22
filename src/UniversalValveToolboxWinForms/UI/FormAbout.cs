using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UniversalValveToolbox {
    public partial class FormAbout : Form {
        public FormAbout() {
            InitializeComponent();
            labelVersion.Text = Utils.VersionHelper.AssemblyVersion;
            labelTitle.Text = Utils.VersionHelper.AssemblyTitle;
            labelCopy.Text = Utils.VersionHelper.AssemblyCopyright;
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
    }
}
