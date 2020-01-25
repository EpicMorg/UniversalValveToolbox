using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Dto;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Model.ViewModel;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox {
    public partial class FormMain : Form {
        private EngineDtoModel[] Engines;
        private ProjectDtoModel[] Projects;

        private DataProvider dataProvider = new DataProvider();

        private ListViewGroup listViewGroupAddons;
        private ListViewGroup listViewGroupTools;

        public FormMain() {
            InitializeComponent();
            FillBaseMenuItems();
            GetInfoForToolbar();

            UpdateEngineList();
            UpdateProjectList();
            UpdateToolsList();
            UpdateAddonsList();

            Text = VersionHelper.AssemblyTitle + VersionHelper.AssemblyVersion;
            comboBoxGameConfig.SelectedIndex = 0;

            comboBoxEngine.SelectedIndexChanged += (s, e) => {
                UpdateProjectList();
                UpdateToolsList();
                UpdateAddonsList();
            };
        }
        private void FormMain_Load(object sender, EventArgs e) {

        }

        private void GetInfoForToolbar() {
            UpdateteamStatus();
        }

        private void toolStripStatusLabelRefresh_Click(object sender, EventArgs e) {
            GetInfoForToolbar();
        }

        public void FillBaseMenuItems() {
            #region static content, do not edit
            //creating groups (categores)
            listViewGroupAddons = new ListViewGroup(Properties.translations.MenuCategories.catAddons);
            ListViewGroup listViewGroupCompileDecpmpile = new ListViewGroup(Properties.translations.MenuCategories.catCompileDecompile);
            ListViewGroup listViewGroupContent = new ListViewGroup(Properties.translations.MenuCategories.catContent);
            ListViewGroup listViewGroupDocs = new ListViewGroup(Properties.translations.MenuCategories.catDocs);
            ListViewGroup listViewGroupLandscape = new ListViewGroup(Properties.translations.MenuCategories.catLandscape);
            ListViewGroup listViewGroupMisc = new ListViewGroup(Properties.translations.MenuCategories.catMisc);
            ListViewGroup listViewGroupSettings = new ListViewGroup(Properties.translations.MenuCategories.catSettings);
            ListViewGroup listViewGroupSupport = new ListViewGroup(Properties.translations.MenuCategories.catSupport);
            ListViewGroup listViewGroupTextures = new ListViewGroup(Properties.translations.MenuCategories.catTextures);
            listViewGroupTools = new ListViewGroup(Properties.translations.MenuCategories.catTools);
            ListViewGroup listViewGroupUtils = new ListViewGroup(Properties.translations.MenuCategories.catUtils);
            ListViewGroup listViewGroupWebLinks = new ListViewGroup(Properties.translations.MenuCategories.catWebLinks);

            //add names to categories
            listViewGroupSettings.Name = "ListViewGroupSettings";
            listViewGroupWebLinks.Name = "ListViewGroupUrls";
            #endregion

            //creating permanent menu items
            ListViewItem listViewItemSettings = new ListViewItem(Properties.translations.MenuItems.itmOpenSettings, 2);
            ListViewItem listViewItemEditConfigurations = new ListViewItem(Properties.translations.MenuItems.itmEditConfigurations, 3);
            ListViewItem listViewItemEditPlugins = new ListViewItem(Properties.translations.MenuItems.itmEditPlugins, 4);
            ListViewItem listViewItemAbout = new ListViewItem(Properties.translations.MenuItems.itmAbout, 5);
            ListViewItem listViewItemGitHubLink = new ListViewItem(Properties.translations.MenuItems.itmGitHubLink, 0);

            //add item to category(group)
            listViewItemSettings.Group = listViewGroupSettings;
            listViewItemEditConfigurations.Group = listViewGroupSettings;
            listViewItemEditPlugins.Group = listViewGroupSettings;

            listViewItemAbout.Group = listViewGroupSupport;
            listViewItemGitHubLink.Group = listViewGroupSupport;

            //draw items and categories in forms
            listView.Groups.AddRange(new ListViewGroup[] {
                listViewGroupTools,
                listViewGroupAddons,
                listViewGroupCompileDecpmpile,
                listViewGroupContent,
                listViewGroupDocs,
                listViewGroupLandscape,
                listViewGroupMisc,
                listViewGroupSettings,
                listViewGroupSupport,
                listViewGroupTextures,
                listViewGroupUtils,
                listViewGroupWebLinks,
            });
            listView.Items.AddRange(new ListViewItem[] {
                listViewItemSettings,
                listViewItemEditConfigurations,
                listViewItemEditPlugins,
                listViewItemGitHubLink,
                listViewItemAbout
            });
        }

        private void UpdateEngineList() {
            var dataProvider = new DataProvider();
            Engines = dataProvider.Engines;

            if (Engines != null && Engines.Length != 0) {
                comboBoxEngine.Enabled = true;
                comboBoxEngine.Items.Clear();
                comboBoxEngine.Items.AddRange(Engines.Select(engine => engine.Name).ToArray());

                comboBoxEngine.SelectedIndex = 0;
            }
        }

        private void UpdateProjectList() {
            Projects = dataProvider.Projects;

            var selectEngine = Engines[comboBoxEngine.SelectedIndex];
            var availableProject = Projects.Where(project => project.Engine == selectEngine.Appid).ToArray();

            if (availableProject != null && availableProject.Length != 0) {
                comboBoxGameConfig.Enabled = true;

                comboBoxGameConfig.Items.Clear();
                comboBoxGameConfig.Items.AddRange(availableProject.Select(project => project.Name).ToArray());

                comboBoxGameConfig.SelectedIndex = 0;
            }
            else {
                comboBoxGameConfig.Enabled = false;
                comboBoxGameConfig.Items.Clear();
            }
        }

        private void UpdateToolsList() {
            var selectEngine = Engines[comboBoxEngine.SelectedIndex];

            var removeItem = new List<ListViewItem>();

            foreach (ListViewItem item in listViewGroupTools.Items) {
                removeItem.Add(item);
            }

            removeItem.ForEach(item => listView.Items.Remove(item));


            var itemsTools = selectEngine.Tools.Select(tool => new ListViewItem(tool.Name, listViewGroupTools)).ToArray();
            listView.Items.AddRange(itemsTools);
        }

        private void UpdateAddonsList() {
            var selectEngine = Engines[comboBoxEngine.SelectedIndex];

            var removeItem = new List<ListViewItem>();

            foreach (ListViewItem item in listViewGroupAddons.Items) {
                removeItem.Add(item);
            }

            removeItem.ForEach(item => listView.Items.Remove(item));

            var itemsAddons = dataProvider.Addons.Where(a => a.Engines.Contains(selectEngine.Appid)).Select(tool => new ListViewItem(tool.Name, listViewGroupAddons)).ToArray();
            listView.Items.AddRange(itemsAddons);
        }

        private void UpdateteamStatus() {
            var steamData = SteamManager.SteamData;

            if (steamData.SteamPid != 0) {
                toolStripStatusLabelSteam.Image = Properties.Resources.checked_16;
                toolStripStatusLabelSteam.Text = Properties.translations.MenuNavbar.menuStrSteam + Properties.translations.MenuNavbar.menuStrOnline;
                toolStripStatusLabelSteam.ToolTipText = Properties.translations.MenuNavbar.menuStrSteam + $"PID: {steamData.SteamPid.ToString()}";
            }
            else {
                toolStripStatusLabelSteam.Image = Properties.Resources.cancel_16;
                toolStripStatusLabelSteam.ToolTipText = "";
                toolStripStatusLabelSteam.Text = Properties.translations.MenuNavbar.menuStrSteam + Properties.translations.MenuNavbar.menuStrOffline;
            }

            if (steamData.UserNameSteam != null) {
                toolStripStatusLabelLogin.Text = Properties.translations.MenuNavbar.menuStrLogin + $"{steamData.UserNameSteam}";
            }
            else {
                toolStripStatusLabelLogin.Text = Properties.translations.MenuNavbar.menuStrLogin + Properties.translations.MenuNavbar.menuStrNone;
            }
        }

        private void button_Launch_Click(object sender, EventArgs e) {
            this.OpenSettings();
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e) {
            var selectItemText = listView.SelectedItems[0].Text;

            if (selectItemText == Properties.translations.MenuItems.itmOpenSettings) {
                this.OpenSettings();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmEditConfigurations) {
                var frmProfiles = new FormProfiles();
                frmProfiles.ShowDialog();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmEditPlugins) {
                var frmPlugins = new FormAddons();
                frmPlugins.ShowDialog();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmAbout) {
                var frmAbout = new FormAbout();
                frmAbout.ShowDialog();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmGitHubLink) {
                Process.Start("https://github.com/EpicMorg/UniversalValveToolbox");
            }
        }

        private void OpenSettings() {
            var dataManager = new DataProvider();
            var settingsDto = dataManager.Settings;
            var languageProvider = new LanguageProvider();

            var settingsModel = new SettingsViewModel(settingsDto, languageProvider);

            var frmSettings = new FormSettings(settingsModel);

            if (frmSettings.ShowDialog() == DialogResult.OK) {
                dataManager.Settings = settingsDto;
                Application.Restart();
            }
        }


    }
}
