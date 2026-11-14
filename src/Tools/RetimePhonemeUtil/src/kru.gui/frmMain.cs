namespace ConverterGui;

using kru.gui;
using kru.gui.Core;

public partial class FrmMain : Form
{
    public FrmMain() => this.InitializeComponent();

    private void BtnBrowseOutputClick(object sender, EventArgs e) => this.txtOutput.Text = this.fbBrowseOutput.ShowDialog() == DialogResult.OK ? this.fbBrowseOutput.SelectedPath : string.Empty;

    private void BtnBrowseOutputCleanClick(object sender, EventArgs e) => this.txtOutputClean.Text = this.fbBrowseOutput.ShowDialog() == DialogResult.OK ? this.fbBrowseOutput.SelectedPath : string.Empty;

    private void BtnBrowseInputClick(object sender, EventArgs e) => this.txtInput.Text = this.fbBrowseInput.ShowDialog() == DialogResult.OK ? this.fbBrowseInput.SelectedPath : string.Empty;

    private void TxtOutputTextChanged(object sender, EventArgs e)
    {
        this.fbBrowseOutput.SelectedPath = this.txtOutput.Text;
        this.EnableButtons();
    }

    private void TxtInputTextChanged(object sender, EventArgs e)
    {
        this.fbBrowseInput.SelectedPath = this.txtInput.Text;
        this.EnableButtons();
    }

    private void BtnSaveClick(object sender, EventArgs e)
    {
        var commands = this.GetCommands();

        if (this.sfdOutput.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllLines(this.sfdOutput.FileName, commands
                .Select(a => a.GetBatchLine()));
        }

        MessageBox.Show("Done!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void EnableButtons()
    {
        var enabled = !string.IsNullOrWhiteSpace(this.txtInput.Text)
            && !string.IsNullOrWhiteSpace(this.txtOutput.Text)
            && Directory.Exists(this.txtInput.Text)
            && Directory.Exists(this.txtOutput.Text)
            && (!this.chkClean.Checked || (!string.IsNullOrWhiteSpace(this.txtOutputClean.Text) && Directory.Exists(this.txtOutputClean.Text)));
        this.btnSave.Enabled = this.btnStart.Enabled = enabled;
    }

    private Command[] GetCommands()
    {
        var options = new
        {
            Input = this.txtInput.Text,
            Output = this.txtOutput.Text,
            OutputClean = this.txtOutputClean.Text,
            Clean = this.chkClean.Checked,
            SaveLog = this.chkSaveLogs.Checked,
        };

        var commands = Directory
            .GetFiles(options.Input, "*.wav")
            .Select(inputFile => new Command(
                InputFile: inputFile,
                OutputDirectory: options.Output,
                CleanDirectory: options.Clean ? options.OutputClean : default,
                SaveLog: options.SaveLog))
            .ToArray();

        return commands;
    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        this.Enabled = false;
        try
        {
            var commands = this.GetCommands();
            foreach (var command in commands)
            {
                var proc = command.GetProcess();
                try
                {
                    proc.Start();
                    await proc.WaitForExitAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error", $"An error has occured while trying to launch command: {ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            MessageBox.Show("Done!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            this.Enabled = true;
        }
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
        var (version, name, _) = ApplicationInfoProvider.GetInfo();
        this.Text = $"{name} {version} ({BitnessProvider.BitnessName})";
    }

    private void chkClean_CheckedChanged(object sender, EventArgs e)
    {
        this.txtOutputClean.Enabled = this.btnBrowseOutputClean.Enabled = this.chkClean.Checked;
        this.EnableButtons();
    }

    private void buttonAbout_Click(object sender, EventArgs e)
    {
        using var about = new FrmAbout();
        about.ShowDialog();
    }
}
