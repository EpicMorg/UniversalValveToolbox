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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.comboBox_Mod = new System.Windows.Forms.ComboBox();
            this.comboBox_Engine = new System.Windows.Forms.ComboBox();
            this.label_Engine = new System.Windows.Forms.Label();
            this.label_Mod = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelSteam = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRefresh = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Mod
            // 
            this.comboBox_Mod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Mod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Mod.FormattingEnabled = true;
            this.comboBox_Mod.Location = new System.Drawing.Point(103, 389);
            this.comboBox_Mod.Name = "comboBox_Mod";
            this.comboBox_Mod.Size = new System.Drawing.Size(242, 21);
            this.comboBox_Mod.TabIndex = 1;
            // 
            // comboBox_Engine
            // 
            this.comboBox_Engine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Engine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Engine.FormattingEnabled = true;
            this.comboBox_Engine.Location = new System.Drawing.Point(103, 362);
            this.comboBox_Engine.Name = "comboBox_Engine";
            this.comboBox_Engine.Size = new System.Drawing.Size(242, 21);
            this.comboBox_Engine.TabIndex = 1;
            // 
            // label_Engine
            // 
            this.label_Engine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Engine.AutoSize = true;
            this.label_Engine.Location = new System.Drawing.Point(17, 365);
            this.label_Engine.Name = "label_Engine";
            this.label_Engine.Size = new System.Drawing.Size(80, 13);
            this.label_Engine.TabIndex = 2;
            this.label_Engine.Text = "Engine version:";
            // 
            // label_Mod
            // 
            this.label_Mod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Mod.AutoSize = true;
            this.label_Mod.Location = new System.Drawing.Point(17, 392);
            this.label_Mod.Name = "label_Mod";
            this.label_Mod.Size = new System.Drawing.Size(75, 13);
            this.label_Mod.TabIndex = 3;
            this.label_Mod.Text = "Current Game:";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageListLarge;
            this.listView.Location = new System.Drawing.Point(12, 12);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(333, 344);
            this.listView.SmallImageList = this.imageListSmall;
            this.listView.StateImageList = this.imageListLarge;
            this.listView.TabIndex = 4;
            this.listView.TileSize = new System.Drawing.Size(256, 30);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "github-logo_24.png");
            this.imageListLarge.Images.SetKeyName(1, "internet_24.png");
            this.imageListLarge.Images.SetKeyName(2, "settings_24.png");
            this.imageListLarge.Images.SetKeyName(3, "technical-support_24.png");
            this.imageListLarge.Images.SetKeyName(4, "plug-silhouette_24.png");
            this.imageListLarge.Images.SetKeyName(5, "info_24.png");
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "github-logo_16.png");
            this.imageListSmall.Images.SetKeyName(1, "internet_16.png");
            this.imageListSmall.Images.SetKeyName(2, "settings_16.png");
            this.imageListSmall.Images.SetKeyName(3, "technical-support_16.png");
            this.imageListSmall.Images.SetKeyName(4, "plug-silhouette_16.png");
            this.imageListSmall.Images.SetKeyName(5, "info_16.png");
            // 
            // statusStrip
            // 
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelSteam,
            this.toolStripStatusLabelLogin,
            this.toolStripStatusLabelRefresh});
            this.statusStrip.Location = new System.Drawing.Point(0, 410);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(357, 25);
            this.statusStrip.TabIndex = 5;
            // 
            // toolStripStatusLabelSteam
            // 
            this.toolStripStatusLabelSteam.Image = global::UniversalValveToolbox.Properties.Resources.cancel_16;
            this.toolStripStatusLabelSteam.Name = "toolStripStatusLabelSteam";
            this.toolStripStatusLabelSteam.Size = new System.Drawing.Size(89, 20);
            this.toolStripStatusLabelSteam.Text = "Steam: none";
            // 
            // toolStripStatusLabelLogin
            // 
            this.toolStripStatusLabelLogin.Image = global::UniversalValveToolbox.Properties.Resources.human_16;
            this.toolStripStatusLabelLogin.Name = "toolStripStatusLabelLogin";
            this.toolStripStatusLabelLogin.Size = new System.Drawing.Size(86, 20);
            this.toolStripStatusLabelLogin.Text = "Login: none";
            // 
            // toolStripStatusLabelRefresh
            // 
            this.toolStripStatusLabelRefresh.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelRefresh.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabelRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabelRefresh.DoubleClickEnabled = true;
            this.toolStripStatusLabelRefresh.Image = global::UniversalValveToolbox.Properties.Resources.refresh_16;
            this.toolStripStatusLabelRefresh.Name = "toolStripStatusLabelRefresh";
            this.toolStripStatusLabelRefresh.Size = new System.Drawing.Size(20, 20);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 435);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.label_Mod);
            this.Controls.Add(this.label_Engine);
            this.Controls.Add(this.comboBox_Engine);
            this.Controls.Add(this.comboBox_Mod);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Universal Valve Toolbox";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_Mod;
        private System.Windows.Forms.ComboBox comboBox_Engine;
        private System.Windows.Forms.Label label_Engine;
        private System.Windows.Forms.Label label_Mod;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSteam;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelLogin;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRefresh;
    }
}

