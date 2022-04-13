namespace UniversalValveToolbox {
    partial class FormSettings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.labelDivider = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelLang = new System.Windows.Forms.Label();
            this.comboBoxLang = new System.Windows.Forms.ComboBox();
            this.labelTranslationBy = new System.Windows.Forms.Label();
            this.labelTranslationAthor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDivider
            // 
            resources.ApplyResources(this.labelDivider, "labelDivider");
            this.labelDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDivider.Name = "labelDivider";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // labelLang
            // 
            resources.ApplyResources(this.labelLang, "labelLang");
            this.labelLang.Name = "labelLang";
            // 
            // comboBoxLang
            // 
            resources.ApplyResources(this.comboBoxLang, "comboBoxLang");
            this.comboBoxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLang.FormattingEnabled = true;
            this.comboBoxLang.Items.AddRange(new object[] {
            resources.GetString("comboBoxLang.Items")});
            this.comboBoxLang.Name = "comboBoxLang";
            // 
            // labelTranslationBy
            // 
            resources.ApplyResources(this.labelTranslationBy, "labelTranslationBy");
            this.labelTranslationBy.Name = "labelTranslationBy";
            // 
            // labelTranslationAthor
            // 
            resources.ApplyResources(this.labelTranslationAthor, "labelTranslationAthor");
            this.labelTranslationAthor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTranslationAthor.Name = "labelTranslationAthor";
            // 
            // FormSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTranslationAthor);
            this.Controls.Add(this.labelTranslationBy);
            this.Controls.Add(this.comboBoxLang);
            this.Controls.Add(this.labelLang);
            this.Controls.Add(this.labelDivider);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDivider;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelLang;
        private System.Windows.Forms.ComboBox comboBoxLang;
        private System.Windows.Forms.Label labelTranslationBy;
        private System.Windows.Forms.Label labelTranslationAthor;
    }
}