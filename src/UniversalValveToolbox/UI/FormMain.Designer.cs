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
            this.comboBoxGameConfig = new System.Windows.Forms.ComboBox();
            this.comboBoxEngine = new System.Windows.Forms.ComboBox();
            this.listView = new System.Windows.Forms.ListView();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelSteam = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelEngines = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRefresh = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxGameConfig
            // 
            this.comboBoxGameConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxGameConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGameConfig.Enabled = false;
            this.comboBoxGameConfig.FormattingEnabled = true;
            this.comboBoxGameConfig.Items.AddRange(new object[] {
            "No project configured"});
            this.comboBoxGameConfig.Location = new System.Drawing.Point(12, 392);
            this.comboBoxGameConfig.Name = "comboBoxGameConfig";
            this.comboBoxGameConfig.Size = new System.Drawing.Size(400, 21);
            this.comboBoxGameConfig.TabIndex = 1;
            // 
            // comboBoxEngine
            // 
            this.comboBoxEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEngine.Enabled = false;
            this.comboBoxEngine.FormattingEnabled = true;
            this.comboBoxEngine.Items.AddRange(new object[] {
            "No engine installed"});
            this.comboBoxEngine.Location = new System.Drawing.Point(12, 365);
            this.comboBoxEngine.Name = "comboBoxEngine";
            this.comboBoxEngine.Size = new System.Drawing.Size(400, 21);
            this.comboBoxEngine.TabIndex = 1;
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
            this.listView.Size = new System.Drawing.Size(400, 347);
            this.listView.SmallImageList = this.imageListSmall;
            this.listView.StateImageList = this.imageListLarge;
            this.listView.TabIndex = 4;
            this.listView.TileSize = new System.Drawing.Size(320, 30);
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
            this.toolStripStatusLabelEngines,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelRefresh});
            this.statusStrip.Location = new System.Drawing.Point(0, 421);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(424, 25);
            this.statusStrip.TabIndex = 5;
            // 
            // toolStripStatusLabelSteam
            // 
            this.toolStripStatusLabelSteam.Image = global::UniversalValveToolbox.Properties.Resources.cancel_16;
            this.toolStripStatusLabelSteam.Name = "toolStripStatusLabelSteam";
            this.toolStripStatusLabelSteam.Size = new System.Drawing.Size(89, 20);
            this.toolStripStatusLabelSteam.Text = "Steam: none";
            this.toolStripStatusLabelSteam.ToolTipText = "666";
            // 
            // toolStripStatusLabelLogin
            // 
            this.toolStripStatusLabelLogin.Image = global::UniversalValveToolbox.Properties.Resources.human_16;
            this.toolStripStatusLabelLogin.Name = "toolStripStatusLabelLogin";
            this.toolStripStatusLabelLogin.Size = new System.Drawing.Size(86, 20);
            this.toolStripStatusLabelLogin.Text = "Login: none";
            // 
            // toolStripStatusLabelEngines
            // 
            this.toolStripStatusLabelEngines.Image = global::UniversalValveToolbox.Properties.Resources.info_16;
            this.toolStripStatusLabelEngines.Name = "toolStripStatusLabelEngines";
            this.toolStripStatusLabelEngines.Size = new System.Drawing.Size(82, 20);
            this.toolStripStatusLabelEngines.Text = "Engines: 00";
            this.toolStripStatusLabelEngines.ToolTipText = "Count of available engines";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Image = global::UniversalValveToolbox.Properties.Resources.plug_silhouette_16;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(82, 20);
            this.toolStripStatusLabel1.Text = "Addons: 00";
            this.toolStripStatusLabel1.ToolTipText = "Count of available plugins";
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
            this.toolStripStatusLabelRefresh.IsLink = true;
            this.toolStripStatusLabelRefresh.Name = "toolStripStatusLabelRefresh";
            this.toolStripStatusLabelRefresh.Size = new System.Drawing.Size(20, 20);
            this.toolStripStatusLabelRefresh.Click += new System.EventHandler(this.toolStripStatusLabelRefresh_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 446);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.comboBoxEngine);
            this.Controls.Add(this.comboBoxGameConfig);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(373, 474);
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
        private System.Windows.Forms.ComboBox comboBoxGameConfig;
        private System.Windows.Forms.ComboBox comboBoxEngine;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSteam;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelLogin;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRefresh;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelEngines;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

