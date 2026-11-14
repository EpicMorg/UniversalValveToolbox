using System.Diagnostics;
using System.Runtime.InteropServices; 

namespace SourceSDK.ENV.Editor.UI
{
    public partial class FormAbout : Form
    {
        string verDarkUI = "Unknown";
        string verOctokit = "Unknown";
        public FormAbout()
        {
            InitializeComponent();
            this.Text = String.Format("{0} {1} ({2})", "About ::", System.Windows.Forms.Application.ProductName, System.Windows.Forms.Application.ProductVersion);
            darkLabelProductVersion.Text = String.Format("{0} ({1})", System.Windows.Forms.Application.ProductName, System.Windows.Forms.Application.ProductVersion);
            darkLabelCopyright.Text = String.Format(" Copyright {1} \u00a9 {0}", System.Windows.Forms.Application.CompanyName, DateTime.Now.Year);
            try
            {
                 verDarkUI = System.Reflection.AssemblyName.GetAssemblyName(Path.Combine(Application.StartupPath,"DarkUI.dll")).Version.ToString();
                 verOctokit = System.Reflection.AssemblyName.GetAssemblyName(Path.Combine(Application.StartupPath, "Octokit.dll")).Version.ToString();
            }
            catch
            {
                verDarkUI = "Unknown";
                verOctokit = "Unknown";
            }
            darkTextBoxDUV.Text = verDarkUI;
            textBoxOKitV.Text = verOctokit;
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {

        }

        private void darkButtonDonate_Click(object sender, EventArgs e)
        {
            var url = "https://ko-fi.com/epicmorg";
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
            
        }
        private void darkButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
