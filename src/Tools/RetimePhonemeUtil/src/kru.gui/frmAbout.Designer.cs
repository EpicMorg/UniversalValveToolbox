namespace kru.gui
{
    partial class FrmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCopy = new System.Windows.Forms.Label();
            this.linkLabelSource = new System.Windows.Forms.LinkLabel();
            this.linkLabelwowks = new System.Windows.Forms.LinkLabel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelappName = new System.Windows.Forms.Label();
            this.labelappVer = new System.Windows.Forms.Label();
            this.linkLabelGitHub = new System.Windows.Forms.LinkLabel();
            this.linkLabelFreepik = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::kru.gui.Properties.Resources.favicon_64;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Original author:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Original source:";
            // 
            // labelCopy
            // 
            this.labelCopy.AutoSize = true;
            this.labelCopy.Location = new System.Drawing.Point(82, 44);
            this.labelCopy.Name = "labelCopy";
            this.labelCopy.Size = new System.Drawing.Size(58, 15);
            this.labelCopy.TabIndex = 3;
            this.labelCopy.Text = "copyright";
            // 
            // linkLabelSource
            // 
            this.linkLabelSource.AutoSize = true;
            this.linkLabelSource.Location = new System.Drawing.Point(108, 124);
            this.linkLabelSource.Name = "linkLabelSource";
            this.linkLabelSource.Size = new System.Drawing.Size(268, 15);
            this.linkLabelSource.TabIndex = 4;
            this.linkLabelSource.TabStop = true;
            this.linkLabelSource.Text = "https://forum.csmania.ru/viewtopic.php?t=43274";
            this.linkLabelSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSource_LinkClicked);
            // 
            // linkLabelwowks
            // 
            this.linkLabelwowks.AutoSize = true;
            this.linkLabelwowks.Location = new System.Drawing.Point(108, 109);
            this.linkLabelwowks.Name = "linkLabelwowks";
            this.linkLabelwowks.Size = new System.Drawing.Size(43, 15);
            this.linkLabelwowks.TabIndex = 4;
            this.linkLabelwowks.TabStop = true;
            this.linkLabelwowks.Text = "wowks";
            this.linkLabelwowks.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelwowks_LinkClicked);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(316, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelappName
            // 
            this.labelappName.AutoSize = true;
            this.labelappName.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelappName.Location = new System.Drawing.Point(82, 9);
            this.labelappName.Name = "labelappName";
            this.labelappName.Size = new System.Drawing.Size(76, 20);
            this.labelappName.TabIndex = 6;
            this.labelappName.Text = "appName";
            // 
            // labelappVer
            // 
            this.labelappVer.AutoSize = true;
            this.labelappVer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelappVer.Location = new System.Drawing.Point(82, 29);
            this.labelappVer.Name = "labelappVer";
            this.labelappVer.Size = new System.Drawing.Size(44, 15);
            this.labelappVer.TabIndex = 6;
            this.labelappVer.Text = "appVer";
            // 
            // linkLabelGitHub
            // 
            this.linkLabelGitHub.AutoSize = true;
            this.linkLabelGitHub.Location = new System.Drawing.Point(316, 38);
            this.linkLabelGitHub.Name = "linkLabelGitHub";
            this.linkLabelGitHub.Size = new System.Drawing.Size(45, 15);
            this.linkLabelGitHub.TabIndex = 7;
            this.linkLabelGitHub.TabStop = true;
            this.linkLabelGitHub.Text = "GitHub";
            this.linkLabelGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGitHub_LinkClicked);
            // 
            // linkLabelFreepik
            // 
            this.linkLabelFreepik.AutoSize = true;
            this.linkLabelFreepik.Location = new System.Drawing.Point(12, 79);
            this.linkLabelFreepik.Name = "linkLabelFreepik";
            this.linkLabelFreepik.Size = new System.Drawing.Size(45, 15);
            this.linkLabelFreepik.TabIndex = 7;
            this.linkLabelFreepik.TabStop = true;
            this.linkLabelFreepik.Text = "Freepik";
            this.linkLabelFreepik.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFreepik_LinkClicked);
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 148);
            this.Controls.Add(this.linkLabelFreepik);
            this.Controls.Add(this.linkLabelGitHub);
            this.Controls.Add(this.labelappVer);
            this.Controls.Add(this.labelappName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.linkLabelwowks);
            this.Controls.Add(this.linkLabelSource);
            this.Controls.Add(this.labelCopy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "appName";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label labelCopy;
        private LinkLabel linkLabelSource;
        private LinkLabel linkLabelwowks;
        private Button buttonOK;
        private Label labelappName;
        private Label labelappVer;
        private LinkLabel linkLabelGitHub;
        private LinkLabel linkLabelFreepik;
    }
}