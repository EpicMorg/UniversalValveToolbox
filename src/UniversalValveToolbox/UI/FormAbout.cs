namespace UniversalValveToolbox
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            this.InitializeComponent();
            this.labelVersion.Text = Utils.VersionHelper.AssemblyVersion;
            this.labelTitle.Text = Utils.VersionHelper.AssemblyTitle;
            this.labelCopy.Text = Utils.VersionHelper.AssemblyCopyright;
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
        }

        private void ButtonOK_Click(object sender, EventArgs e) => this.Close();

        private void LinkLabelIconSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://www.flaticon.com/");

        private void LinkLabelFP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://www.flaticon.com/authors/freepik");

        private void LinkLabelTI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://www.flaticon.com/authors/those-icons");
    }
}
