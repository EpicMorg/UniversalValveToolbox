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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.button_Settings = new System.Windows.Forms.Button();
            this.comboBox_Mod = new System.Windows.Forms.ComboBox();
            this.comboBox_Engine = new System.Windows.Forms.ComboBox();
            this.label_Engine = new System.Windows.Forms.Label();
            this.label_Mod = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // button_Settings
            // 
            this.button_Settings.Location = new System.Drawing.Point(270, 169);
            this.button_Settings.Name = "button_Settings";
            this.button_Settings.Size = new System.Drawing.Size(75, 21);
            this.button_Settings.TabIndex = 0;
            this.button_Settings.Text = "Settings";
            this.button_Settings.UseVisualStyleBackColor = true;
            this.button_Settings.Click += new System.EventHandler(this.button_Launch_Click);
            // 
            // comboBox_Mod
            // 
            this.comboBox_Mod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Mod.FormattingEnabled = true;
            this.comboBox_Mod.Location = new System.Drawing.Point(103, 142);
            this.comboBox_Mod.Name = "comboBox_Mod";
            this.comboBox_Mod.Size = new System.Drawing.Size(242, 21);
            this.comboBox_Mod.TabIndex = 1;
            // 
            // comboBox_Engine
            // 
            this.comboBox_Engine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Engine.FormattingEnabled = true;
            this.comboBox_Engine.Location = new System.Drawing.Point(103, 115);
            this.comboBox_Engine.Name = "comboBox_Engine";
            this.comboBox_Engine.Size = new System.Drawing.Size(242, 21);
            this.comboBox_Engine.TabIndex = 1;
            // 
            // label_Engine
            // 
            this.label_Engine.AutoSize = true;
            this.label_Engine.Location = new System.Drawing.Point(17, 118);
            this.label_Engine.Name = "label_Engine";
            this.label_Engine.Size = new System.Drawing.Size(80, 13);
            this.label_Engine.TabIndex = 2;
            this.label_Engine.Text = "Engine version:";
            this.label_Engine.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_Mod
            // 
            this.label_Mod.AutoSize = true;
            this.label_Mod.Location = new System.Drawing.Point(17, 145);
            this.label_Mod.Name = "label_Mod";
            this.label_Mod.Size = new System.Drawing.Size(75, 13);
            this.label_Mod.TabIndex = 3;
            this.label_Mod.Text = "Current Game:";
            this.label_Mod.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "ListViewGroup";
            listViewGroup3.Name = "listViewGroup3";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(333, 97);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 200);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label_Mod);
            this.Controls.Add(this.label_Engine);
            this.Controls.Add(this.comboBox_Engine);
            this.Controls.Add(this.comboBox_Mod);
            this.Controls.Add(this.button_Settings);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Universal Valve Toolbox";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Settings;
        private System.Windows.Forms.ComboBox comboBox_Mod;
        private System.Windows.Forms.ComboBox comboBox_Engine;
        private System.Windows.Forms.Label label_Engine;
        private System.Windows.Forms.Label label_Mod;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}

