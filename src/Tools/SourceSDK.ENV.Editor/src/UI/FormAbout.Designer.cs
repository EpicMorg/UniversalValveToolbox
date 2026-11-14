namespace SourceSDK.ENV.Editor.UI
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.darkLabelProductVersion = new DarkUI.Controls.DarkLabel();
            this.darkButtonOK = new DarkUI.Controls.DarkButton();
            this.darkLabelCopyright = new DarkUI.Controls.DarkLabel();
            this.darkButtonDonate = new DarkUI.Controls.DarkButton();
            this.groupBoxAddons = new DarkUI.Controls.DarkGroupBox();
            this.darkTextBoxDUV = new DarkUI.Controls.DarkTextBox();
            this.textBoxOKitV = new DarkUI.Controls.DarkTextBox();
            this.darkTextBoxDU = new DarkUI.Controls.DarkTextBox();
            this.textBoxOKit = new DarkUI.Controls.DarkTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxAddons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SourceSDK.ENV.Editor.Properties.Resources.crowbar_64;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // darkLabelProductVersion
            // 
            this.darkLabelProductVersion.AutoSize = true;
            this.darkLabelProductVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.darkLabelProductVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabelProductVersion.Location = new System.Drawing.Point(82, 12);
            this.darkLabelProductVersion.Name = "darkLabelProductVersion";
            this.darkLabelProductVersion.Size = new System.Drawing.Size(241, 16);
            this.darkLabelProductVersion.TabIndex = 2;
            this.darkLabelProductVersion.Text = "Application.ProductName (0.0.0.0)";
            // 
            // darkButtonOK
            // 
            this.darkButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.darkButtonOK.Location = new System.Drawing.Point(12, 114);
            this.darkButtonOK.Name = "darkButtonOK";
            this.darkButtonOK.Padding = new System.Windows.Forms.Padding(5);
            this.darkButtonOK.Size = new System.Drawing.Size(64, 23);
            this.darkButtonOK.TabIndex = 3;
            this.darkButtonOK.Text = "OK";
            this.darkButtonOK.Click += new System.EventHandler(this.darkButtonOK_Click);
            // 
            // darkLabelCopyright
            // 
            this.darkLabelCopyright.AutoSize = true;
            this.darkLabelCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.darkLabelCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabelCopyright.Location = new System.Drawing.Point(82, 28);
            this.darkLabelCopyright.Name = "darkLabelCopyright";
            this.darkLabelCopyright.Size = new System.Drawing.Size(166, 15);
            this.darkLabelCopyright.TabIndex = 2;
            this.darkLabelCopyright.Text = "Copyright (Company) (c) Year";
            // 
            // darkButtonDonate
            // 
            this.darkButtonDonate.Location = new System.Drawing.Point(12, 82);
            this.darkButtonDonate.Name = "darkButtonDonate";
            this.darkButtonDonate.Padding = new System.Windows.Forms.Padding(5);
            this.darkButtonDonate.Size = new System.Drawing.Size(64, 23);
            this.darkButtonDonate.TabIndex = 3;
            this.darkButtonDonate.Text = "Donate";
            this.darkButtonDonate.Click += new System.EventHandler(this.darkButtonDonate_Click);
            // 
            // groupBoxAddons
            // 
            this.groupBoxAddons.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.groupBoxAddons.Controls.Add(this.darkTextBoxDUV);
            this.groupBoxAddons.Controls.Add(this.textBoxOKitV);
            this.groupBoxAddons.Controls.Add(this.darkTextBoxDU);
            this.groupBoxAddons.Controls.Add(this.textBoxOKit);
            this.groupBoxAddons.Location = new System.Drawing.Point(82, 50);
            this.groupBoxAddons.Name = "groupBoxAddons";
            this.groupBoxAddons.Size = new System.Drawing.Size(318, 87);
            this.groupBoxAddons.TabIndex = 5;
            this.groupBoxAddons.TabStop = false;
            this.groupBoxAddons.Text = "Addons:";
            // 
            // darkTextBoxDUV
            // 
            this.darkTextBoxDUV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkTextBoxDUV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkTextBoxDUV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkTextBoxDUV.Location = new System.Drawing.Point(159, 22);
            this.darkTextBoxDUV.Name = "darkTextBoxDUV";
            this.darkTextBoxDUV.PlaceholderText = "0.0.0.0";
            this.darkTextBoxDUV.ReadOnly = true;
            this.darkTextBoxDUV.Size = new System.Drawing.Size(147, 23);
            this.darkTextBoxDUV.TabIndex = 0;
            // 
            // textBoxOKitV
            // 
            this.textBoxOKitV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBoxOKitV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOKitV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBoxOKitV.Location = new System.Drawing.Point(159, 51);
            this.textBoxOKitV.Name = "textBoxOKitV";
            this.textBoxOKitV.PlaceholderText = "0.0.0.0";
            this.textBoxOKitV.ReadOnly = true;
            this.textBoxOKitV.Size = new System.Drawing.Size(147, 23);
            this.textBoxOKitV.TabIndex = 0;
            // 
            // darkTextBoxDU
            // 
            this.darkTextBoxDU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkTextBoxDU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.darkTextBoxDU.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkTextBoxDU.Location = new System.Drawing.Point(6, 22);
            this.darkTextBoxDU.Name = "darkTextBoxDU";
            this.darkTextBoxDU.PlaceholderText = "custom.dll";
            this.darkTextBoxDU.ReadOnly = true;
            this.darkTextBoxDU.Size = new System.Drawing.Size(147, 23);
            this.darkTextBoxDU.TabIndex = 0;
            this.darkTextBoxDU.Text = "DarkUI.dll";
            // 
            // textBoxOKit
            // 
            this.textBoxOKit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBoxOKit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOKit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBoxOKit.Location = new System.Drawing.Point(6, 51);
            this.textBoxOKit.Name = "textBoxOKit";
            this.textBoxOKit.PlaceholderText = "custom.dll";
            this.textBoxOKit.ReadOnly = true;
            this.textBoxOKit.Size = new System.Drawing.Size(147, 23);
            this.textBoxOKit.TabIndex = 0;
            this.textBoxOKit.Text = "Ocktokit.dll";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(409, 149);
            this.Controls.Add(this.groupBoxAddons);
            this.Controls.Add(this.darkButtonDonate);
            this.Controls.Add(this.darkButtonOK);
            this.Controls.Add(this.darkLabelCopyright);
            this.Controls.Add(this.darkLabelProductVersion);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxAddons.ResumeLayout(false);
            this.groupBoxAddons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private DarkUI.Controls.DarkLabel darkLabelProductVersion;
        private DarkUI.Controls.DarkButton darkButtonOK;
        private DarkUI.Controls.DarkLabel darkLabelCopyright;
        private DarkUI.Controls.DarkButton darkButtonDonate;
        private DarkUI.Controls.DarkGroupBox groupBoxAddons;
        private DarkUI.Controls.DarkTextBox darkTextBoxDUV;
        private DarkUI.Controls.DarkTextBox textBoxOKitV;
        private DarkUI.Controls.DarkTextBox darkTextBoxDU;
        private DarkUI.Controls.DarkTextBox textBoxOKit;
    }
}