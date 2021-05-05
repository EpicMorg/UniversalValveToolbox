using EpicMorg.SteamPathsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Dto;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Model.ViewModel;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox {
    public partial class FormMain : Form {
        private readonly string RUN_PROJECT_ID = "RUN_PROJECT_ID";

        private EngineDtoModel[] Engines;
        private ProjectDtoModel[] Projects;
        private ProjectDtoModel[] AvailableProjects;

        private DataProvider dataProvider = new DataProvider();

        private ListViewGroup listViewGroupAddons;
        private ListViewGroup listViewGroupTools;

        private EngineDtoModel SelectedEngine {
            get {
                if (Engines.Length == 0)
                    return null;

                return Engines[comboBoxEngine.SelectedIndex];
            }
        }
        private ProjectDtoModel SelectedProject {
            get => (ProjectDtoModel)((comboBoxProjects.Enabled)
                ? Projects.First(project => project.Name.Equals(comboBoxProjects.SelectedItem))
                : null);
        }

        public FormMain() {
            InitializeComponent();
            FillBaseMenuItems();

            UpdateFormData();

            Text = VersionHelper.AssemblyTitle + VersionHelper.AssemblyVersion;

            comboBoxEngine.SelectedIndexChanged += (s, e) => {
                UpdateProjectList();
                UpdateToolsList();
                UpdateAddonsList();
            };
        }

        private void FormMain_Load(object sender, EventArgs e) {

        }

        private void UpdateLastSelectedProject() {
            var lastSelectedProject = dataProvider.Projects.FirstOrDefault(project => project.Name.Equals(dataProvider.Settings.LastSelectedProject));

            if (lastSelectedProject != null && Engines.Length != 0) {
                var indexEngine = comboBoxEngine.Items.IndexOf(Engines.First(engine => engine.Appid.Equals(lastSelectedProject.Engine)).Name);
                comboBoxEngine.SelectedIndex = indexEngine;

                UpdateProjectList();
                UpdateToolsList();

                var indexProject = comboBoxProjects.Items.IndexOf(lastSelectedProject.Name);
                comboBoxProjects.SelectedIndex = indexProject;

                EnvUtils.PrepareVProject($"{SelectedProject?.Path ?? string.Empty}");
                EnvUtils.PrepareSFMData($"{SelectedProject?.Path ?? string.Empty}");
            }
        }
        private void SaveLastSelectedProject() {
            var settings = dataProvider.Settings;
            settings.LastSelectedProject = SelectedProject?.Name;
            dataProvider.Settings = settings;
        }


        private void UpdateFormData() {
            UpdateEngineList();
            UpdateProjectList();
            UpdateToolsList();
            UpdateAddonsList();
            UpdateNavigationBar();

            UpdateLastSelectedProject();
        }

        private void UpdateNavigationBar() {
            UpdateLogInStatus();
            UpdateInfoNavigationBar();
        }

        private void toolStripStatusLabelRefresh_Click(object sender, EventArgs e) {
            UpdateFormData();
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
            ListViewItem listViewItemGitHubReport = new ListViewItem(Properties.translations.MenuItems.itmGitHubReport, 0);

            //add item to category(group)
            listViewItemSettings.Group = listViewGroupSettings;
            listViewItemEditConfigurations.Group = listViewGroupSettings;
            listViewItemEditPlugins.Group = listViewGroupSettings;

            listViewItemAbout.Group = listViewGroupSupport;
            listViewItemGitHubLink.Group = listViewGroupSupport;
            listViewItemGitHubReport.Group = listViewGroupSupport;

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
                listViewItemGitHubReport,
                listViewItemAbout
            });
        }

        private void UpdateEngineList() {
            var dataProvider = new DataProvider();
            Engines = dataProvider.Engines.Where(engine => {
                var engineAppData = SteamPathsUtil.GetSteamAppDataById(engine.Appid);

                return engineAppData != null && engineAppData.Installed;
             }).ToArray();

            if (Engines != null && Engines.Length != 0) {
                comboBoxEngine.Enabled = true;
                comboBoxEngine.Items.Clear();
                comboBoxEngine.Items.AddRange(Engines.Select(engine => engine.Name).ToArray());

                comboBoxEngine.SelectedIndex = 0;
            }
        }

        private void UpdateProjectList() {
            if (Engines.Length == 0) {
                comboBoxProjects.Enabled = false;

                comboBoxProjects.Items.Clear();

                return;
            }

            Projects = dataProvider.Projects;

            var selectEngine = Engines[comboBoxEngine.SelectedIndex];
            AvailableProjects = Projects.Where(project => project.Engine == selectEngine.Appid).ToArray();

            if (AvailableProjects != null && AvailableProjects.Length != 0) {
                comboBoxProjects.Enabled = true;

                comboBoxProjects.Items.Clear();
                comboBoxProjects.Items.AddRange(AvailableProjects.Select(project => project.Name).ToArray());

                comboBoxProjects.SelectedIndex = 0;
            }
            else {
                comboBoxProjects.Enabled = false;

                comboBoxProjects.Items.Clear();
            }
        }

        private void UpdateToolsList() {
            var removeItem = new List<ListViewItem>();

            foreach (ListViewItem item in listViewGroupTools.Items) {
                removeItem.Add(item);
            }

            removeItem.ForEach(item => listView.Items.Remove(item));

            if (SelectedEngine == null)
                return;

            var pathSelectedEngine = SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid)?.Path;

            if (pathSelectedEngine != null) {
                var pairPathIconTools = SelectedEngine.Tools
                    .Where(tool => File.Exists(Path.Combine(pathSelectedEngine, tool.Bin)))
                    .Select(tool => {
                        var keyByPath = Path.Combine(pathSelectedEngine, tool.Bin);
                        var icon = Icon.ExtractAssociatedIcon(Path.Combine(pathSelectedEngine, tool.Bin));

                        return new Pair(keyByPath, icon);
                    });

                foreach (var pair in pairPathIconTools) {
                    listView.SmallImageList.Images.Add((string)pair.First, (Icon)pair.Second);
                    listView.LargeImageList.Images.Add((string)pair.First, (Icon)pair.Second);
                }
            }

            var itemsTools = SelectedEngine.Tools
                .Where(tool => File.Exists(Path.Combine(pathSelectedEngine, tool.Bin)))

                .Select(tool => {
                    string keyByPath = null;

                    if (pathSelectedEngine != null)
                        keyByPath = Path.Combine(pathSelectedEngine, tool.Bin);

                    if (keyByPath == null) {
                        return new ListViewItem(tool.Name, listViewGroupTools);
                    }
                    else {
                        return new ListViewItem(tool.Name, keyByPath, listViewGroupTools);
                    }

                }).ToArray();
            listView.Items.AddRange(itemsTools);


            var engineData = SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid);
            var isAvailableProjectBySelectEngine = Projects.Any(project => project.Engine == SelectedEngine.Appid);

            if (engineData != null && isAvailableProjectBySelectEngine) {
                var enginePath = engineData.Path;

                if (enginePath != null) {
                    var iconPathEngine = Path.Combine(enginePath, SelectedEngine.Bin);

                    var runProjectListViewItem = new ListViewItem(Properties.translations.MenuItems.itmRunProject, iconPathEngine, listViewGroupTools);
                    runProjectListViewItem.Tag = RUN_PROJECT_ID;

                    listView.Items.Add(runProjectListViewItem);
                }
            }
        }

        private void UpdateAddonsList() {
            var removeItem = new List<ListViewItem>();

            foreach (ListViewItem item in listViewGroupAddons.Items) {
                removeItem.Add(item);
            }

            removeItem.ForEach(item => listView.Items.Remove(item));

            if (SelectedEngine == null)
                return;

            var pathSelectedEngine = SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid)?.Path;
            var addonsSelectedEngine = dataProvider.Addons.Where(a => a.Engines.Contains(SelectedEngine.Appid));


            var pairPathIconTools = addonsSelectedEngine
                .Where(addon => {
                    var path = addon.Bin;

                    return File.Exists(path);

                })
                .Select(addons => {
                    var keyByPath = addons.Bin;
                    var icon = Icon.ExtractAssociatedIcon(keyByPath);

                    return new Pair(keyByPath, icon);
                });

            foreach (var pair in pairPathIconTools) {
                listView.SmallImageList.Images.Add((string)pair.First, (Icon)pair.Second);
                listView.LargeImageList.Images.Add((string)pair.First, (Icon)pair.Second);
            }

            var itemsAddons = addonsSelectedEngine.Select(addons => {
                string keyByPath = null;

                if (pathSelectedEngine != null)
                    keyByPath = Path.Combine(pathSelectedEngine, addons.Bin);

                if (keyByPath == null) {
                    return new ListViewItem(addons.Name, listViewGroupAddons);
                }
                else {
                    return new ListViewItem(addons.Name, keyByPath, listViewGroupAddons);
                }

            }).ToArray();

            listView.Items.AddRange(itemsAddons);
        }

        private void UpdateInfoNavigationBar() {
            var countAvailableEngines = Engines.Length;
            var countAvailableProjects = dataProvider.Projects.Length;
            var countAvailableAddons = dataProvider.Addons.Length;

            toolStripStatusLabelEngines.Text = Properties.translations.MenuNavbar.menuStrEngines + $"{countAvailableEngines}";
            toolStripStatusLabelAddons.Text = Properties.translations.MenuNavbar.menuStrAddons + $"{countAvailableAddons}";
        }

        private void UpdateLogInStatus() {
            var steamData = SteamManager.SteamData;

            if (steamData.SteamPid != 0) {
                toolStripStatusLabelSteam.Image = Properties.Resources.checked_16;
                toolStripStatusLabelSteam.Text = Properties.translations.MenuNavbar.menuStrSteam + Properties.translations.MenuNavbar.menuStrOnline;
                toolStripStatusLabelSteam.ToolTipText = Properties.translations.MenuNavbar.menuStrSteam + $"PID: {steamData.SteamPid.ToString()}";
            }
            else {
                toolStripStatusLabelSteam.Image = Properties.Resources.cancel_16;
                toolStripStatusLabelSteam.ToolTipText = string.Empty;
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
            var selectItem = listView.SelectedItems[0];

            var selectItemText = selectItem.Text;
            EnvUtils.PrepareVProject($"{SelectedProject?.Path ?? string.Empty}");
            EnvUtils.PrepareSFMData($"{SelectedProject?.Path ?? string.Empty}");

            if (selectItem.Group == listViewGroupTools) {
                if (RUN_PROJECT_ID.Equals(selectItem.Tag) && SelectedProject != null) {
                    var pathEngineBin = Path.Combine(SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid).Path, SelectedEngine.Bin);

                    
                    Process.Start(pathEngineBin, $"-steam -game \"{SelectedProject?.Path ?? string.Empty}\" {SelectedProject.Args}");
                }

                var selectedTool = SelectedEngine.Tools.FirstOrDefault(tool => tool.Name == selectItemText);
                if (selectedTool != null) {
                    var selectedEnginePath = SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid)?.Path;

                    if (selectedEnginePath != null) {
                        var toolPath = Path.Combine(selectedEnginePath, selectedTool.Bin);

                        if (File.Exists(toolPath)) {
                            string finalArg = $"-steam {selectedTool.Args}";

                            if (!finalArg.Contains("-game")) {
                                finalArg += $" -game \"{SelectedProject?.Path ?? string.Empty}\" ";
                            }

                            Process.Start(toolPath, finalArg);
                        }
                        else
                            MessageBox.Show($"\"{selectedTool.Name}\" {Properties.translations.MessageBoxes.msgTextNotFound}\n{toolPath}", Properties.translations.MessageBoxes.msgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else {
                        DialogResult dialogResult = MessageBox.Show($"\"{SelectedEngine.Name}\" {Properties.translations.MessageBoxes.msgTextWithAppID} \"{SelectedEngine.Appid}\" {Properties.translations.MessageBoxes.msgTextNotInstalledInstall}", Properties.translations.MessageBoxes.msgWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes) {
                            Process.Start($"steam://install/{SelectedEngine.Appid}");
                        }
                        else if (dialogResult == DialogResult.No) {
                            MessageBox.Show($"{Properties.translations.MessageBoxes.msgTextInstallationOf} \"{SelectedEngine.Name}\" {Properties.translations.MessageBoxes.msgTextWithAppID} \"{SelectedEngine.Appid}\" {Properties.translations.MessageBoxes.msgTextCancelled}", Properties.translations.MessageBoxes.msgInfo, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }

            }
            else if (selectItem.Group == listViewGroupAddons) {
                var selectedAddons = dataProvider.Addons.FirstOrDefault(addon => addon.Name == selectItemText);
                if (selectedAddons != null) {
                    var addonPath = Path.Combine(selectedAddons.Bin);

                    if (File.Exists(addonPath))
                        Process.Start(addonPath, selectedAddons.Args);
                    else
                        MessageBox.Show($"\"{selectedAddons.Name}\" {Properties.translations.MessageBoxes.msgTextNotFound}\n{addonPath}", Properties.translations.MessageBoxes.msgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }

            else if (selectItemText == Properties.translations.MenuItems.itmOpenSettings) {
                this.OpenSettings();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmEditConfigurations) {
                var frmProfiles = new FormProjects();
                if (frmProfiles.ShowDialog() == DialogResult.OK) {
                    UpdateFormData();
                }
            }
            else if (selectItemText == Properties.translations.MenuItems.itmEditPlugins) {
                var frmPlugins = new FormAddons();
                if (frmPlugins.ShowDialog() == DialogResult.OK) {
                    UpdateFormData();
                }
            }
            else if (selectItemText == Properties.translations.MenuItems.itmAbout) {
                var frmAbout = new FormAbout();
                frmAbout.ShowDialog();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmGitHubLink) {
                Process.Start("https://github.com/EpicMorg/UniversalValveToolbox");
            }
            else if (selectItemText == Properties.translations.MenuItems.itmGitHubReport) {
                Process.Start("https://github.com/EpicMorg/UniversalValveToolbox/issues/new/choose");
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            SaveLastSelectedProject();
        }
    }
}
