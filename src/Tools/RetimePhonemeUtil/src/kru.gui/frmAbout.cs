namespace kru.gui;

using System;
using System.Diagnostics;
using System.Windows.Forms;

using kru.gui.Core;

public partial class FrmAbout : Form
{
    public FrmAbout()
    {
        this.InitializeComponent();
    }

    private static void OpenUrl(string url)
    {
        var uri = new Uri(url);
        if (uri.Scheme is not "http" and not "https")
        {
            throw new ArgumentException("Invalid URL", nameof(url));
        }

        try
        {
            using var myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = uri.ToString();
            myProcess.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void frmAbout_Load(object sender, EventArgs e)
    {
        var (version, name, copyright) = ApplicationInfoProvider.GetInfo();

        this.Text = $"{name} {version} ({BitnessProvider.BitnessName})";
        this.labelappName.Text = $"{name} ({BitnessProvider.BitnessName})";
        this.labelappVer.Text = version;
        this.labelCopy.Text = $"Copyright © {copyright}";
    }

    private void linkLabelSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://forum.csmania.ru/viewtopic.php?t=43274");

    private void linkLabelwowks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://forum.csmania.ru/memberlist.php?mode=viewprofile&u=59383");

    private void buttonOK_Click(object sender, EventArgs e) => this.Close();

    private void linkLabelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://github.com/AntinomyCollective/Retime-Phoneme-Util-GUI");

    private void linkLabelFreepik_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => OpenUrl("https://www.freepik.com/");
}
