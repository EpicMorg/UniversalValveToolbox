namespace ConverterGui;

partial class FrmMain
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.fbBrowseInput = new System.Windows.Forms.FolderBrowserDialog();
            this.fbBrowseOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdOutput = new System.Windows.Forms.SaveFileDialog();
            this.chkSaveLogs = new System.Windows.Forms.CheckBox();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.chkClean = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOutputClean = new System.Windows.Forms.TextBox();
            this.btnBrowseOutputClean = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point(5, 21);
            this.txtInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(292, 23);
            this.txtInput.TabIndex = 1;
            this.txtInput.TextChanged += new System.EventHandler(this.TxtInputTextChanged);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(5, 21);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(292, 23);
            this.txtOutput.TabIndex = 3;
            this.txtOutput.TextChanged += new System.EventHandler(this.TxtOutputTextChanged);
            // 
            // btnBrowseInput
            // 
            this.btnBrowseInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseInput.Location = new System.Drawing.Point(301, 21);
            this.btnBrowseInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseInput.TabIndex = 4;
            this.btnBrowseInput.Text = "Browse";
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.BtnBrowseInputClick);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOutput.Location = new System.Drawing.Point(301, 21);
            this.btnBrowseOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutput.TabIndex = 5;
            this.btnBrowseOutput.Text = "Browse";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.BtnBrowseOutputClick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(239, 233);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save batch";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(318, 233);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // sfdOutput
            // 
            this.sfdOutput.Filter = "\"Batch File|*.bat|All files|*.*\"";
            // 
            // chkSaveLogs
            // 
            this.chkSaveLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSaveLogs.AutoSize = true;
            this.chkSaveLogs.Checked = true;
            this.chkSaveLogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveLogs.Location = new System.Drawing.Point(92, 236);
            this.chkSaveLogs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkSaveLogs.Name = "chkSaveLogs";
            this.chkSaveLogs.Size = new System.Drawing.Size(75, 19);
            this.chkSaveLogs.TabIndex = 9;
            this.chkSaveLogs.Text = "Save logs";
            this.chkSaveLogs.UseVisualStyleBackColor = true;
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInput.Controls.Add(this.txtInput);
            this.groupBoxInput.Controls.Add(this.btnBrowseInput);
            this.groupBoxInput.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(381, 63);
            this.groupBoxInput.TabIndex = 10;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input folder:";
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOutput.Controls.Add(this.txtOutput);
            this.groupBoxOutput.Controls.Add(this.btnBrowseOutput);
            this.groupBoxOutput.Location = new System.Drawing.Point(12, 81);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(381, 63);
            this.groupBoxOutput.TabIndex = 11;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output folder:";
            // 
            // chkClean
            // 
            this.chkClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkClean.AutoSize = true;
            this.chkClean.Location = new System.Drawing.Point(5, 50);
            this.chkClean.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkClean.Name = "chkClean";
            this.chkClean.Size = new System.Drawing.Size(112, 19);
            this.chkClean.TabIndex = 9;
            this.chkClean.Text = "Save Clean copy";
            this.chkClean.UseVisualStyleBackColor = true;
            this.chkClean.CheckedChanged += new System.EventHandler(this.chkClean_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtOutputClean);
            this.groupBox1.Controls.Add(this.btnBrowseOutputClean);
            this.groupBox1.Controls.Add(this.chkClean);
            this.groupBox1.Location = new System.Drawing.Point(12, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 74);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output folder for clean files (Optional):";
            // 
            // txtOutputClean
            // 
            this.txtOutputClean.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputClean.Enabled = false;
            this.txtOutputClean.Location = new System.Drawing.Point(5, 21);
            this.txtOutputClean.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOutputClean.Name = "txtOutputClean";
            this.txtOutputClean.Size = new System.Drawing.Size(292, 23);
            this.txtOutputClean.TabIndex = 3;
            this.txtOutputClean.TextChanged += new System.EventHandler(this.TxtOutputTextChanged);
            // 
            // btnBrowseOutputClean
            // 
            this.btnBrowseOutputClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOutputClean.Enabled = false;
            this.btnBrowseOutputClean.Location = new System.Drawing.Point(301, 21);
            this.btnBrowseOutputClean.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBrowseOutputClean.Name = "btnBrowseOutputClean";
            this.btnBrowseOutputClean.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOutputClean.TabIndex = 5;
            this.btnBrowseOutputClean.Text = "Browse";
            this.btnBrowseOutputClean.UseVisualStyleBackColor = true;
            this.btnBrowseOutputClean.Click += new System.EventHandler(this.BtnBrowseOutputCleanClick);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAbout.Location = new System.Drawing.Point(12, 233);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(75, 23);
            this.buttonAbout.TabIndex = 12;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 267);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkSaveLogs);
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSave);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(418, 299);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "appName";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private TextBox txtInput;
    private TextBox txtOutput;
    private Button btnBrowseInput;
    private Button btnBrowseOutput;
    private Button btnSave;
    private Button btnStart;
    private FolderBrowserDialog fbBrowseInput;
    private FolderBrowserDialog fbBrowseOutput;
    private SaveFileDialog sfdOutput;
    private CheckBox chkSaveLogs;
    private GroupBox groupBoxInput;
    private GroupBox groupBoxOutput;
    private CheckBox chkClean;
    private GroupBox groupBox1;
    private TextBox txtOutputClean;
    private Button btnBrowseOutputClean;
    private Button buttonAbout;
}
