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
            this.comboBoxProjects = new System.Windows.Forms.ComboBox();
            this.comboBoxEngine = new System.Windows.Forms.ComboBox();
            this.listView = new System.Windows.Forms.ListView();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelSteam = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelEngines = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelAddons = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRefresh = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxProjects
            // 
            resources.ApplyResources(this.comboBoxProjects, "comboBoxProjects");
            this.comboBoxProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProjects.FormattingEnabled = true;
            this.comboBoxProjects.Items.AddRange(new object[] {
            resources.GetString("comboBoxProjects.Items")});
            this.comboBoxProjects.Name = "comboBoxProjects";
            // 
            // comboBoxEngine
            // 
            resources.ApplyResources(this.comboBoxEngine, "comboBoxEngine");
            this.comboBoxEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEngine.FormattingEnabled = true;
            this.comboBoxEngine.Items.AddRange(new object[] {
            resources.GetString("comboBoxEngine.Items")});
            this.comboBoxEngine.Name = "comboBoxEngine";
            // 
            // listView
            // 
            resources.ApplyResources(this.listView, "listView");
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageListLarge;
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.SmallImageList = this.imageListSmall;
            this.listView.StateImageList = this.imageListLarge;
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
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelSteam,
            this.toolStripStatusLabelLogin,
            this.toolStripStatusLabelEngines,
            this.toolStripStatusLabelAddons,
            this.toolStripStatusLabelRefresh});
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            // 
            // toolStripStatusLabelSteam
            // 
            resources.ApplyResources(this.toolStripStatusLabelSteam, "toolStripStatusLabelSteam");
            this.toolStripStatusLabelSteam.Image = global::UniversalValveToolbox.Properties.Resources.cancel_16;
            this.toolStripStatusLabelSteam.Margin = new System.Windows.Forms.Padding(11, 3, 0, 2);
            this.toolStripStatusLabelSteam.Name = "toolStripStatusLabelSteam";
            // 
            // toolStripStatusLabelLogin
            // 
            resources.ApplyResources(this.toolStripStatusLabelLogin, "toolStripStatusLabelLogin");
            this.toolStripStatusLabelLogin.Image = global::UniversalValveToolbox.Properties.Resources.human_16;
            this.toolStripStatusLabelLogin.Name = "toolStripStatusLabelLogin";
            // 
            // toolStripStatusLabelEngines
            // 
            resources.ApplyResources(this.toolStripStatusLabelEngines, "toolStripStatusLabelEngines");
            this.toolStripStatusLabelEngines.Image = global::UniversalValveToolbox.Properties.Resources.info_16;
            this.toolStripStatusLabelEngines.Name = "toolStripStatusLabelEngines";
            // 
            // toolStripStatusLabelAddons
            // 
            resources.ApplyResources(this.toolStripStatusLabelAddons, "toolStripStatusLabelAddons");
            this.toolStripStatusLabelAddons.Image = global::UniversalValveToolbox.Properties.Resources.plug_silhouette_16;
            this.toolStripStatusLabelAddons.Name = "toolStripStatusLabelAddons";
            // 
            // toolStripStatusLabelRefresh
            // 
            resources.ApplyResources(this.toolStripStatusLabelRefresh, "toolStripStatusLabelRefresh");
            this.toolStripStatusLabelRefresh.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelRefresh.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabelRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabelRefresh.DoubleClickEnabled = true;
            this.toolStripStatusLabelRefresh.Image = global::UniversalValveToolbox.Properties.Resources.refresh_16;
            this.toolStripStatusLabelRefresh.IsLink = true;
            this.toolStripStatusLabelRefresh.Name = "toolStripStatusLabelRefresh";
            this.toolStripStatusLabelRefresh.Click += new System.EventHandler(this.toolStripStatusLabelRefresh_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.comboBoxEngine);
            this.Controls.Add(this.comboBoxProjects);
            this.DoubleBuffered = true;
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxProjects;
        private System.Windows.Forms.ComboBox comboBoxEngine;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSteam;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelLogin;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRefresh;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelEngines;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAddons;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

