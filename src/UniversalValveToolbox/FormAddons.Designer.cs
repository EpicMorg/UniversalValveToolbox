namespace UniversalValveToolbox
{
    partial class FormAddons
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddons));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.comboBox_Addon = new System.Windows.Forms.ComboBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.labelAddon = new System.Windows.Forms.Label();
            this.labelLinkedEngine = new System.Windows.Forms.Label();
            this.dataGridViewEngines = new System.Windows.Forms.DataGridView();
            this.ColumnEngineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAppID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.textBoxArgs = new System.Windows.Forms.TextBox();
            this.labelArgs = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelDivider = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEngines)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(408, 382);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(327, 382);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // comboBox_Addon
            // 
            this.comboBox_Addon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Addon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Addon.FormattingEnabled = true;
            this.comboBox_Addon.Location = new System.Drawing.Point(12, 25);
            this.comboBox_Addon.Name = "comboBox_Addon";
            this.comboBox_Addon.Size = new System.Drawing.Size(390, 21);
            this.comboBox_Addon.TabIndex = 6;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(408, 23);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 10;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            // 
            // buttonNew
            // 
            this.buttonNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNew.Location = new System.Drawing.Point(408, 52);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(75, 23);
            this.buttonNew.TabIndex = 9;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            // 
            // labelAddon
            // 
            this.labelAddon.AutoSize = true;
            this.labelAddon.Location = new System.Drawing.Point(12, 9);
            this.labelAddon.Name = "labelAddon";
            this.labelAddon.Size = new System.Drawing.Size(74, 13);
            this.labelAddon.TabIndex = 11;
            this.labelAddon.Text = "Select Addon:";
            // 
            // labelLinkedEngine
            // 
            this.labelLinkedEngine.AutoSize = true;
            this.labelLinkedEngine.Location = new System.Drawing.Point(12, 166);
            this.labelLinkedEngine.Name = "labelLinkedEngine";
            this.labelLinkedEngine.Size = new System.Drawing.Size(78, 13);
            this.labelLinkedEngine.TabIndex = 12;
            this.labelLinkedEngine.Text = "Linked Engine:";
            // 
            // dataGridViewEngines
            // 
            this.dataGridViewEngines.AllowUserToAddRows = false;
            this.dataGridViewEngines.AllowUserToDeleteRows = false;
            this.dataGridViewEngines.AllowUserToOrderColumns = true;
            this.dataGridViewEngines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEngines.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewEngines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEngines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnEngineName,
            this.ColumnAppID});
            this.dataGridViewEngines.Location = new System.Drawing.Point(12, 182);
            this.dataGridViewEngines.Name = "dataGridViewEngines";
            this.dataGridViewEngines.ReadOnly = true;
            this.dataGridViewEngines.Size = new System.Drawing.Size(471, 180);
            this.dataGridViewEngines.TabIndex = 13;
            // 
            // ColumnEngineName
            // 
            this.ColumnEngineName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnEngineName.HeaderText = "Engine";
            this.ColumnEngineName.Name = "ColumnEngineName";
            this.ColumnEngineName.ReadOnly = true;
            this.ColumnEngineName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnAppID
            // 
            this.ColumnAppID.FillWeight = 256F;
            this.ColumnAppID.HeaderText = "AppID";
            this.ColumnAppID.Name = "ColumnAppID";
            this.ColumnAppID.ReadOnly = true;
            this.ColumnAppID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnAppID.Width = 62;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 49);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(12, 65);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(390, 20);
            this.textBoxName.TabIndex = 15;
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(12, 104);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(390, 20);
            this.textBoxPath.TabIndex = 17;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 88);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(32, 13);
            this.labelPath.TabIndex = 16;
            this.labelPath.Text = "Path:";
            // 
            // textBoxArgs
            // 
            this.textBoxArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArgs.Location = new System.Drawing.Point(12, 143);
            this.textBoxArgs.Name = "textBoxArgs";
            this.textBoxArgs.Size = new System.Drawing.Size(390, 20);
            this.textBoxArgs.TabIndex = 19;
            // 
            // labelArgs
            // 
            this.labelArgs.AutoSize = true;
            this.labelArgs.Location = new System.Drawing.Point(12, 127);
            this.labelArgs.Name = "labelArgs";
            this.labelArgs.Size = new System.Drawing.Size(60, 13);
            this.labelArgs.TabIndex = 18;
            this.labelArgs.Text = "Arguments:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(408, 102);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 20;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            // 
            // labelDivider
            // 
            this.labelDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDivider.Location = new System.Drawing.Point(-16, 373);
            this.labelDivider.Name = "labelDivider";
            this.labelDivider.Size = new System.Drawing.Size(550, 2);
            this.labelDivider.TabIndex = 24;
            // 
            // FormAddons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 417);
            this.Controls.Add(this.labelDivider);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxArgs);
            this.Controls.Add(this.labelArgs);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.dataGridViewEngines);
            this.Controls.Add(this.labelLinkedEngine);
            this.Controls.Add(this.labelAddon);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBox_Addon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddons";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Addons";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEngines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ComboBox comboBox_Addon;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Label labelAddon;
        private System.Windows.Forms.Label labelLinkedEngine;
        private System.Windows.Forms.DataGridView dataGridViewEngines;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEngineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAppID;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.TextBox textBoxArgs;
        private System.Windows.Forms.Label labelArgs;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelDivider;
    }
}