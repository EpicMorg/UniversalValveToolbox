namespace UniversalValveToolbox
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.button_Launch = new System.Windows.Forms.Button();
            this.comboBox_Mod = new System.Windows.Forms.ComboBox();
            this.comboBox_Engine = new System.Windows.Forms.ComboBox();
            this.label_Engine = new System.Windows.Forms.Label();
            this.label_Mod = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Launch
            // 
            this.button_Launch.Location = new System.Drawing.Point(265, 72);
            this.button_Launch.Name = "button_Launch";
            this.button_Launch.Size = new System.Drawing.Size(75, 23);
            this.button_Launch.TabIndex = 0;
            this.button_Launch.Text = "Launch";
            this.button_Launch.UseVisualStyleBackColor = true;
            this.button_Launch.Click += new System.EventHandler(this.button_Launch_Click);
            // 
            // comboBox_Mod
            // 
            this.comboBox_Mod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Mod.FormattingEnabled = true;
            this.comboBox_Mod.Location = new System.Drawing.Point(72, 45);
            this.comboBox_Mod.Name = "comboBox_Mod";
            this.comboBox_Mod.Size = new System.Drawing.Size(268, 21);
            this.comboBox_Mod.TabIndex = 1;
            // 
            // comboBox_Engine
            // 
            this.comboBox_Engine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Engine.FormattingEnabled = true;
            this.comboBox_Engine.Location = new System.Drawing.Point(72, 12);
            this.comboBox_Engine.Name = "comboBox_Engine";
            this.comboBox_Engine.Size = new System.Drawing.Size(268, 21);
            this.comboBox_Engine.TabIndex = 1;
            // 
            // label_Engine
            // 
            this.label_Engine.AutoSize = true;
            this.label_Engine.Location = new System.Drawing.Point(12, 15);
            this.label_Engine.Name = "label_Engine";
            this.label_Engine.Size = new System.Drawing.Size(40, 13);
            this.label_Engine.TabIndex = 2;
            this.label_Engine.Text = "Engine";
            this.label_Engine.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_Mod
            // 
            this.label_Mod.AutoSize = true;
            this.label_Mod.Location = new System.Drawing.Point(12, 48);
            this.label_Mod.Name = "label_Mod";
            this.label_Mod.Size = new System.Drawing.Size(28, 13);
            this.label_Mod.TabIndex = 3;
            this.label_Mod.Text = "Mod";
            this.label_Mod.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 105);
            this.Controls.Add(this.label_Mod);
            this.Controls.Add(this.label_Engine);
            this.Controls.Add(this.comboBox_Engine);
            this.Controls.Add(this.comboBox_Mod);
            this.Controls.Add(this.button_Launch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Universal Valve Toolbox";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Launch;
        private System.Windows.Forms.ComboBox comboBox_Mod;
        private System.Windows.Forms.ComboBox comboBox_Engine;
        private System.Windows.Forms.Label label_Engine;
        private System.Windows.Forms.Label label_Mod;
    }
}

