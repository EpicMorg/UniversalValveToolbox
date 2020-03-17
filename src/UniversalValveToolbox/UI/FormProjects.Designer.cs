using System;

namespace UniversalValveToolbox {
    partial class FormProjects {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProjects));
            this.comboBox_Mod = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBoxEngine = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelAddon = new System.Windows.Forms.Label();
            this.labelDivider = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.textBoxArgs = new System.Windows.Forms.TextBox();
            this.labelArgs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_Mod
            // 
            resources.ApplyResources(this.comboBox_Mod, "comboBox_Mod");
            this.comboBox_Mod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Mod.FormattingEnabled = true;
            this.comboBox_Mod.Name = "comboBox_Mod";
            this.comboBox_Mod.SelectedIndexChanged += new System.EventHandler(this.comboBox_Mod_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonNew
            // 
            resources.ApplyResources(this.buttonNew, "buttonNew");
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonRemove
            // 
            resources.ApplyResources(this.buttonRemove, "buttonRemove");
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonBrowse
            // 
            resources.ApplyResources(this.buttonBrowse, "buttonBrowse");
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // comboBoxEngine
            // 
            resources.ApplyResources(this.comboBoxEngine, "comboBoxEngine");
            this.comboBoxEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEngine.FormattingEnabled = true;
            this.comboBoxEngine.Name = "comboBoxEngine";
            this.comboBoxEngine.SelectedIndexChanged += new System.EventHandler(this.comboBoxEngine_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // labelAddon
            // 
            resources.ApplyResources(this.labelAddon, "labelAddon");
            this.labelAddon.Name = "labelAddon";
            // 
            // labelDivider
            // 
            resources.ApplyResources(this.labelDivider, "labelDivider");
            this.labelDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelDivider.Name = "labelDivider";
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // textBoxArgs
            // 
            resources.ApplyResources(this.textBoxArgs, "textBoxArgs");
            this.textBoxArgs.Name = "textBoxArgs";
            // 
            // labelArgs
            // 
            resources.ApplyResources(this.labelArgs, "labelArgs");
            this.labelArgs.Name = "labelArgs";
            // 
            // FormProjects
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxArgs);
            this.Controls.Add(this.labelArgs);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.labelDivider);
            this.Controls.Add(this.labelAddon);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBoxEngine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBox_Mod);
            this.Name = "FormProjects";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FormEditProfile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ComboBox comboBox_Mod;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBoxEngine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelAddon;
        private System.Windows.Forms.Label labelDivider;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TextBox textBoxArgs;
        private System.Windows.Forms.Label labelArgs;
    }
}