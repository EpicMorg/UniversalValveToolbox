using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;
using DarkUI.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SourceSDK.ENV.Editor.UI
{
    public partial class FormMain : Form
    {
        GitHubClient client = new GitHubClient(new ProductHeaderValue("SourceSDK.ENV.Editor"));
        Version localVersion = new Version(System.Windows.Forms.Application.ProductVersion); //Replace this with your local version. Only tested with numeric values.
      

        public FormMain()
        {
            InitializeComponent();
            this.Text = String.Format("{0} ({1})", System.Windows.Forms.Application.ProductName, System.Windows.Forms.Application.ProductVersion); ;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            checkAll();
            try  { SilentCheckGitHubNewerVersion(); } catch { }
            folderBrowserDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //set defaut directory
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkAll();
            DarkUI.Forms.DarkMessageBox.ShowInformation("Information updated!", "Information", DarkDialogButton.Ok);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout? frmAbout = new();
            frmAbout.ShowDialog();
        }

        #region update check
        private async System.Threading.Tasks.Task SilentCheckGitHubNewerVersion() 
        {
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("EpicMorg", "SourceSDK.ENV.Editor");
            Version latestGitHubVersion = new Version(releases[0].TagName);
            int versionComparison = localVersion.CompareTo(latestGitHubVersion);
            if (versionComparison < 0)
            {
                toolStripStatusLabelUpdateStatus.Image = Properties.Resources.cancel;
                toolStripStatusLabelUpdateStatus.Text = "New version avalible";
                toolStripStatusLabelUpdateStatus.ToolTipText = "The version on GitHub is more up to date than this local release.";
            }
            else if (versionComparison > 0)
            {
                toolStripStatusLabelUpdateStatus.Image = Properties.Resources.wait;
                toolStripStatusLabelUpdateStatus.Text = "This local version is greater";
                toolStripStatusLabelUpdateStatus.ToolTipText ="This local version is greater than the release version on GitHub.";
            }
            else
            {
                toolStripStatusLabelUpdateStatus.Image = Properties.Resources.done;
                toolStripStatusLabelUpdateStatus.Text = "This version is up to date";
                toolStripStatusLabelUpdateStatus.ToolTipText = "This local Version and the Version on GitHub are equal.";
            }
        }
        private async System.Threading.Tasks.Task CheckGitHubNewerVersion()
        {
            //Get all releases from GitHub
            //Source: https://octokitnet.readthedocs.io/en/latest/getting-started/

            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("EpicMorg", "SourceSDK.ENV.Editor");
            //Setup the versions
            Version latestGitHubVersion = new Version(releases[0].TagName);

            //Compare the Versions
            //Source: https://stackoverflow.com/questions/7568147/compare-version-numbers-without-using-split-function
            int versionComparison = localVersion.CompareTo(latestGitHubVersion);

            if (versionComparison < 0)
            {
                if (DarkUI.Forms.DarkMessageBox.ShowWarning("The version on GitHub is more up to date than this local release. Do you want to update it?", "Information", DarkDialogButton.YesNo) == DialogResult.Yes)
                {
                    var url = "https://github.com/EpicMorg/SourceSDK.ENV.Editor/releases/latest";
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
            }
            else if (versionComparison > 0)
            {
                DarkUI.Forms.DarkMessageBox.ShowInformation("This local version is greater than the release version on GitHub.", "Information", DarkDialogButton.Ok);
                //This local version is greater than the release version on GitHub.
            }
            else
            {
                DarkUI.Forms.DarkMessageBox.ShowInformation("This local Version and the Version on GitHub are equal.", "Information", DarkDialogButton.Ok);
                //This local Version and the Version on GitHub are equal.
            }
        }

        #endregion

        #region checks
        private void checkAll()
        {
            checkVMod();
            checkVProject();
            checkVGame();
            checkVContent();
            checkVTools();
        }
        private void checkVMod()
        {
            var value = Environment.GetEnvironmentVariable("VMod", EnvironmentVariableTarget.User);
            if (value == null)
            {
                textBoxVMod.Text = value;
                pictureBoxVMod.Image = Properties.Resources.cancel;
            }
            else
            {
                textBoxVMod.Text = value;
                pictureBoxVMod.Image = Properties.Resources.done;
            }
        }
        private void checkVProject()
        {
            var value = Environment.GetEnvironmentVariable("VProject", EnvironmentVariableTarget.User);
            if (value == null)
            {
                textBoxVProject.Text = value;
                pictureBoxVProject.Image = Properties.Resources.cancel;
            }
            else
            {
                textBoxVProject.Text = value;
                pictureBoxVProject.Image = Properties.Resources.done;
            }
        }
        private void checkVGame()
        {
            var value = Environment.GetEnvironmentVariable("VGame", EnvironmentVariableTarget.User);
            if (value == null)
            {
                textBoxVGame.Text = value;
                pictureBoxVGame.Image = Properties.Resources.cancel;
            }
            else
            {
                textBoxVGame.Text = value;
                pictureBoxVGame.Image = Properties.Resources.done;
            }
        }
        private void checkVContent()
        {
            var value = Environment.GetEnvironmentVariable("VContent", EnvironmentVariableTarget.User);
            if (value == null)
            {
                textBoxVContent.Text = value;
                pictureBoxVContent.Image = Properties.Resources.cancel;
            }
            else
            {
                textBoxVContent.Text = value;
                pictureBoxVContent.Image = Properties.Resources.done;
            }
        }
        private void checkVTools()
        {
            var value = Environment.GetEnvironmentVariable("VTools", EnvironmentVariableTarget.User);
            if (value == null)
            {
                textBoxVTools.Text = value;
                pictureBoxVTools.Image = Properties.Resources.cancel;
            }
            else
            {
                textBoxVTools.Text = value;
                pictureBoxVTools.Image = Properties.Resources.done;
            }
        }
        #endregion

        #region reset
        private void resetAll()
        {
            resetVMod();
            resetVProject();
            resetVGame();
            resetVContent();
            resetVTools();
        }

        private void resetVMod()
        {
            try
            {
                pictureBoxVMod.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VMod", null, EnvironmentVariableTarget.User);
                    checkVMod();
                }
                catch
                {
                    checkVMod();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VMod successfully cleared and Information updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VMod was not successfully deleted", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        private void resetVProject()
        {
            try
            {
                pictureBoxVProject.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VProject", null, EnvironmentVariableTarget.User);
                    checkVProject();
                }
                catch
                {
                    checkVProject();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VProject successfully cleared and Information updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VProject was not successfully deleted", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        private void resetVGame()
        {
            try
            {
                pictureBoxVGame.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VGame", null, EnvironmentVariableTarget.User);
                    checkVGame();
                }
                catch
                {
                    checkVGame();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VGame successfully cleared and Information updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VGame was not successfully deleted", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        private void resetVContent()
        {
            try
            {
                pictureBoxVContent.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VContent", null, EnvironmentVariableTarget.User);
                    checkVContent();
                }
                catch
                {
                    checkVContent();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VContent successfully cleared and Information updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VContent was not successfully deleted", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        private void resetVTools()
        {
            try
            {
                pictureBoxVTools.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VTools", null, EnvironmentVariableTarget.User);
                    checkVTools();
                }
                catch
                {
                    checkVTools();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VTools successfully cleared and Information updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VTools was not successfully deleted", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        #region apply
        private void applyVMod()
        {
            try
            {
                pictureBoxVMod.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VMod", textBoxVMod.Text, EnvironmentVariableTarget.User);
                    checkVMod();
                }
                catch
                {
                    checkVMod();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VMod successfully updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VMod was not successfully updated", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        private void applyVProject()
        {
            try
            {
                pictureBoxVProject.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VProject", textBoxVProject.Text, EnvironmentVariableTarget.User);
                    checkVProject();
                }
                catch
                {
                    checkVProject();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VProject successfully updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VProject was not successfully updated", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        private void applyVGame()
        {
            try
            {
                pictureBoxVGame.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VGame", textBoxVGame.Text, EnvironmentVariableTarget.User);
                    checkVGame();
                }
                catch
                {
                    checkVGame();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VGame successfully cleared updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VGame was not successfully updated", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        private void applyVContent()
        {
            try
            {
                pictureBoxVContent.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VContent", textBoxVContent.Text, EnvironmentVariableTarget.User);
                    checkVContent();
                }
                catch
                {
                    checkVContent();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VContent successfully updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VContent was not successfully updated", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }

        private void applyVTools()
        {
            try
            {
                pictureBoxVTools.Image = Properties.Resources.wait;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Environment.SetEnvironmentVariable("VTools", textBoxVTools.Text, EnvironmentVariableTarget.User);
                    checkVTools();
                }
                catch
                {
                    checkVTools();
                }

                DarkUI.Forms.DarkMessageBox.ShowInformation("VTools successfully cleared updated!", "Information", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
            catch
            {
                DarkUI.Forms.DarkMessageBox.ShowError("Something went wrong and VTools was not successfully updated", "Error", DarkDialogButton.Ok);
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region btn-reset
        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                resetAll();
            }
        }
        private void buttonResetVMod_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                resetVMod();
            }
           
        }

        private void buttonResetVProject_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                resetVProject();
            }
        }

        private void buttonResetVGame_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                resetVGame();
            }
        }

        private void buttonResetVContent_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                resetVContent();
            }
        }

        private void buttonResetVTools_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                resetVTools();
            }
        }
        #endregion
            
        #region btn-browse
        private void buttonBrowseVProject_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog()== DialogResult.OK)
            {
                textBoxVProject.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonBrowseVGame_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxVGame.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonBrowseVContent_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxVContent.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonBrowseVTools_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxVTools.Text = folderBrowserDialog.SelectedPath;
            }
        }
        #endregion

        #region btn-apply
        private void buttonApplyVMod_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                applyVMod();
            }
        }

        private void buttonApplyVProject_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                applyVProject();
            }
        }

        private void buttonApplyVGame_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                applyVGame();
            }
        }

        private void buttonApplyVContent_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                applyVContent();
            }
        }

        private void buttonApplyVTools_Click(object sender, EventArgs e)
        {
            if (DarkUI.Forms.DarkMessageBox.ShowWarning("Are you sure?", "Warning", DarkDialogButton.YesNo) == DialogResult.Yes)
            {
                applyVTools();
            }
        }
        #endregion

        private void toolStripStatusLabelUpdateStatus_Click(object sender, EventArgs e)
        {
            try { CheckGitHubNewerVersion(); } catch { }
            
        }
    }
}
