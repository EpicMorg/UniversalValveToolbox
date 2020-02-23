using EpicMorg.SteamPathsLib;
using Newtonsoft.Json;
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
        private EngineDtoModel[] Engines;
        private ProjectDtoModel[] Projects;
        private ProjectDtoModel[] AvailableProjects;

        private DataProvider dataProvider = new DataProvider();

        private ListViewGroup listViewGroupAddons;
        private ListViewGroup listViewGroupTools;

        private EngineDtoModel SelectedEngine { get => Engines[comboBoxEngine.SelectedIndex]; }
        private ProjectDtoModel SelectedProject {
            get => (ProjectDtoModel)((comboBoxGameConfig.Enabled)
                ?  comboBoxGameConfig.SelectedItem
                : null); 
                }

        public FormMain() {
            InitializeComponent();
            FillBaseMenuItems();
            GetInfoForToolbar();

            UpdateEngineList();
            UpdateProjectList();
            UpdateToolsList();
            UpdateAddonsList();

            Text = VersionHelper.AssemblyTitle + VersionHelper.AssemblyVersion;

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
            AvailableProjects = Projects.Where(project => project.Engine == selectEngine.Appid).ToArray();

            if (AvailableProjects != null && AvailableProjects.Length != 0) {
                comboBoxGameConfig.Enabled = true;
                runProjectButton.Enabled = true;

                comboBoxGameConfig.Items.Clear();
                comboBoxGameConfig.Items.AddRange(AvailableProjects.ToArray());

                comboBoxGameConfig.SelectedIndex = 0;
            }
            else {
                comboBoxGameConfig.Enabled = false;
                runProjectButton.Enabled = false;
                comboBoxGameConfig.Items.Clear();
            }
        }

        private void UpdateToolsList() {
            var pathSelectedEngine = SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid)?.Path;

            var removeItem = new List<ListViewItem>();

            foreach (ListViewItem item in listViewGroupTools.Items) {
                removeItem.Add(item);
            }

            removeItem.ForEach(item => listView.Items.Remove(item));

            if (pathSelectedEngine != null) {
                var pairPathIconTools = SelectedEngine.Tools
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

            var itemsTools = SelectedEngine.Tools.Select(tool => {
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
        }

        private void UpdateAddonsList() {
            var pathSelectedEngine = SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid)?.Path;
            var addonsSelectedEngine = dataProvider.Addons.Where(a => a.Engines.Contains(SelectedEngine.Appid));

            var removeItem = new List<ListViewItem>();

            foreach (ListViewItem item in listViewGroupAddons.Items) {
                removeItem.Add(item);
            }

            removeItem.ForEach(item => listView.Items.Remove(item));


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
            var selectItem = listView.SelectedItems[0];

            var selectItemText = selectItem.Text;

            if (selectItem.Group == listViewGroupTools) {
                var selectedTool = SelectedEngine.Tools.FirstOrDefault(tool => tool.Name == selectItemText);
                if (selectedTool != null) {
                    var selectedEnginePath = SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid)?.Path;

                    if (selectedEnginePath != null) {
                        var toolPath = Path.Combine(selectedEnginePath, selectedTool.Bin);

                        if (File.Exists(toolPath))
                            Process.Start(toolPath, selectedTool.Args);
                        else
                            MessageBox.Show($"\"{selectedTool.Name}\" no found.\n{toolPath}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else {
                        MessageBox.Show($"\"{SelectedEngine.Name}\" not install", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show($"\"{selectedAddons.Name}\" no found.\n{addonPath}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }

            else if (selectItemText == Properties.translations.MenuItems.itmOpenSettings) {
                this.OpenSettings();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmEditConfigurations) {
                var frmProfiles = new FormProfiles();
                if (frmProfiles.ShowDialog() == DialogResult.OK) {
                    Application.Restart();
                }
            }
            else if (selectItemText == Properties.translations.MenuItems.itmEditPlugins) {
                var frmPlugins = new FormAddons();
                if (frmPlugins.ShowDialog() == DialogResult.OK) {
                    Application.Restart();
                }
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

        private void runProjectButton_Click(object sender, EventArgs e) {
            var pathEngineBin = Path.Combine(SteamPathsUtil.GetSteamAppManifestDataById(SelectedEngine.Appid).Path, SelectedEngine.Bin);

            Process.Start(pathEngineBin, $"-game {SelectedProject.Path} {SelectedProject.Args}");
        }

        private void comboBoxGameConfig_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
