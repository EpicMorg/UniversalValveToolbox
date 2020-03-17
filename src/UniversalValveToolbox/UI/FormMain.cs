namespace UniversalValveToolbox
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using EpicMorg.SteamPathsLib;
    using Properties.translations;
    using UniversalValveToolbox.Model.Dto;
    using UniversalValveToolbox.Model.Provider;
    using UniversalValveToolbox.Model.ViewModel;
    using UniversalValveToolbox.Utils;

    public partial class FormMain : Form
    {
        private readonly string runProjectId = "RUN_PROJECT_ID";

        private EngineDtoModel[] engines;
        private ProjectDtoModel[] projects;
        private ProjectDtoModel[] availableProjects;

        private readonly DataProvider dataProvider = new DataProvider();

        private ListViewGroup listViewGroupAddons;
        private ListViewGroup listViewGroupTools;

        private EngineDtoModel SelectedEngine => this.engines.Length == 0
            ? null
            : this.engines[this.comboBoxEngine.SelectedIndex];

        private ProjectDtoModel SelectedProject => (ProjectDtoModel)(this.comboBoxProjects.Enabled
                ? this.projects.FirstOrDefault(project => project.Name.Equals(this.comboBoxProjects.SelectedItem))
                : null);

        public FormMain()
        {
            this.InitializeComponent();
            this.FillBaseMenuItems();

            this.UpdateFormData();

            this.Text = VersionHelper.AssemblyTitle + VersionHelper.AssemblyVersion;

            this.comboBoxEngine.SelectedIndexChanged += (s, e) =>
            {
                this.UpdateProjectList();
                this.UpdateToolsList();
                this.UpdateAddonsList();
            };
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void UpdateLastSelectedProject()
        {
            var lastSelectedProject = this.dataProvider.Projects.FirstOrDefault(project => project.Name.Equals(this.dataProvider.Settings.LastSelectedProject));

            if (lastSelectedProject != null && this.engines.Length != 0)
            {
                var indexEngine = this.comboBoxEngine.Items.IndexOf(this.engines.First(engine => engine.Appid.Equals(lastSelectedProject.Engine)).Name);
                this.comboBoxEngine.SelectedIndex = indexEngine;

                this.UpdateProjectList();
                this.UpdateToolsList();

                var indexProject = this.comboBoxProjects.Items.IndexOf(lastSelectedProject.Name);
                this.comboBoxProjects.SelectedIndex = indexProject;

                EnvUtils.PrepareVProject($"{this.SelectedProject?.Path ?? string.Empty}");
                EnvUtils.PrepareSFMData($"{this.SelectedProject?.Path ?? string.Empty}");
            }
        }

        private void SaveLastSelectedProject()
        {
            var settings = this.dataProvider.Settings;
            settings.LastSelectedProject = this.SelectedProject?.Name;
            this.dataProvider.Settings = settings;
        }

        private void UpdateFormData()
        {
            this.UpdateEngineList();
            this.UpdateProjectList();
            this.UpdateToolsList();
            this.UpdateAddonsList();
            this.UpdateNavigationBar();

            this.UpdateLastSelectedProject();
        }

        private void UpdateNavigationBar()
        {
            this.UpdateLogInStatus();
            this.UpdateInfoNavigationBar();
        }

        private void ToolStripStatusLabelRefresh_Click(object sender, EventArgs e) => this.UpdateFormData();

        public void FillBaseMenuItems()
        {
            #region static content, do not edit
            // creating groups (categores)
            this.listViewGroupAddons = new ListViewGroup(MenuCategories.catAddons);
            var listViewGroupCompileDecpmpile = new ListViewGroup(MenuCategories.catCompileDecompile);
            var listViewGroupContent = new ListViewGroup(MenuCategories.catContent);
            var listViewGroupDocs = new ListViewGroup(MenuCategories.catDocs);
            var listViewGroupLandscape = new ListViewGroup(MenuCategories.catLandscape);
            var listViewGroupMisc = new ListViewGroup(MenuCategories.catMisc);
            var listViewGroupSettings = new ListViewGroup(MenuCategories.catSettings);
            var listViewGroupSupport = new ListViewGroup(MenuCategories.catSupport);
            var listViewGroupTextures = new ListViewGroup(MenuCategories.catTextures);
            this.listViewGroupTools = new ListViewGroup(MenuCategories.catTools);
            var listViewGroupUtils = new ListViewGroup(MenuCategories.catUtils);
            var listViewGroupWebLinks = new ListViewGroup(MenuCategories.catWebLinks);

            // add names to categories
            listViewGroupSettings.Name = "ListViewGroupSettings";
            listViewGroupWebLinks.Name = "ListViewGroupUrls";
            #endregion

            // creating permanent menu items
            var listViewItemSettings = new ListViewItem(MenuItems.itmOpenSettings, 2);
            var listViewItemEditConfigurations = new ListViewItem(MenuItems.itmEditConfigurations, 3);
            var listViewItemEditPlugins = new ListViewItem(MenuItems.itmEditPlugins, 4);
            var listViewItemAbout = new ListViewItem(MenuItems.itmAbout, 5);
            var listViewItemGitHubLink = new ListViewItem(MenuItems.itmGitHubLink, 0);
            var listViewItemGitHubReport = new ListViewItem(MenuItems.itmGitHubReport, 0);

            // add item to category(group)
            listViewItemSettings.Group = listViewGroupSettings;
            listViewItemEditConfigurations.Group = listViewGroupSettings;
            listViewItemEditPlugins.Group = listViewGroupSettings;

            listViewItemAbout.Group = listViewGroupSupport;
            listViewItemGitHubLink.Group = listViewGroupSupport;
            listViewItemGitHubReport.Group = listViewGroupSupport;

            // draw items and categories in forms
            this.listView.Groups.AddRange(new ListViewGroup[]
            {
                this.listViewGroupTools,
                this.listViewGroupAddons,
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
            this.listView.Items.AddRange(new ListViewItem[]
            {
                listViewItemSettings,
                listViewItemEditConfigurations,
                listViewItemEditPlugins,
                listViewItemGitHubLink,
                listViewItemGitHubReport,
                listViewItemAbout,
            });
        }

        private void UpdateEngineList()
        {
            var dataProvider = new DataProvider();
            this.engines = dataProvider.Engines.Where(engine => SteamPathsUtil.GetSteamAppDataById(engine.Appid) != null).ToArray();

            if (this.engines != null && this.engines.Length != 0)
            {
                this.comboBoxEngine.Enabled = true;
                this.comboBoxEngine.Items.Clear();
                this.comboBoxEngine.Items.AddRange(this.engines.Select(engine => engine.Name).ToArray());

                this.comboBoxEngine.SelectedIndex = 0;
            }
        }

        private void UpdateProjectList()
        {
            if (this.engines.Length == 0)
            {
                this.comboBoxProjects.Enabled = false;

                this.comboBoxProjects.Items.Clear();

                return;
            }

            this.projects = this.dataProvider.Projects;

            var selectEngine = this.engines[this.comboBoxEngine.SelectedIndex];
            this.availableProjects = this.projects.Where(project => project.Engine == selectEngine.Appid).ToArray();

            if (this.availableProjects != null && this.availableProjects.Length != 0)
            {
                this.comboBoxProjects.Enabled = true;

                this.comboBoxProjects.Items.Clear();
                this.comboBoxProjects.Items.AddRange(this.availableProjects.Select(project => project.Name).ToArray());

                this.comboBoxProjects.SelectedIndex = 0;
            }
            else
            {
                this.comboBoxProjects.Enabled = false;

                this.comboBoxProjects.Items.Clear();
            }
        }

        private void UpdateToolsList()
        {
            var removeItem = new List<ListViewItem>();

            foreach (ListViewItem item in this.listViewGroupTools.Items)
            {
                removeItem.Add(item);
            }

            removeItem.ForEach(item => this.listView.Items.Remove(item));

            if (this.SelectedEngine == null)
            {
                return;
            }

            var pathSelectedEngine = SteamPathsUtil.GetSteamAppManifestDataById(this.SelectedEngine.Appid)?.Path;

            if (pathSelectedEngine != null)
            {
                var pairPathIconTools = this.SelectedEngine.Tools
                    .Where(tool => File.Exists(Path.Combine(pathSelectedEngine, tool.Bin)))
                    .Select(tool =>
                    {
                        var keyByPath = Path.Combine(pathSelectedEngine, tool.Bin);
                        var icon = Icon.ExtractAssociatedIcon(Path.Combine(pathSelectedEngine, tool.Bin));

                        return (keyByPath, icon);
                    });

                foreach (var (keyByPath, icon) in pairPathIconTools)
                {
                    this.listView.SmallImageList.Images.Add(keyByPath, icon);
                    this.listView.LargeImageList.Images.Add(keyByPath, icon);
                }
            }

            var itemsTools = this.SelectedEngine.Tools
                .Where(tool => File.Exists(Path.Combine(pathSelectedEngine, tool.Bin)))

                .Select(tool =>
                {
                    string keyByPath = null;

                    if (pathSelectedEngine != null)
                    {
                        keyByPath = Path.Combine(pathSelectedEngine, tool.Bin);
                    }

                    return keyByPath == null
                        ? new ListViewItem(tool.Name, this.listViewGroupTools)
                        : new ListViewItem(tool.Name, keyByPath, this.listViewGroupTools);
                }).ToArray();
            this.listView.Items.AddRange(itemsTools);

            var engineData = SteamPathsUtil.GetSteamAppManifestDataById(this.SelectedEngine.Appid);
            var isAvailableProjectBySelectEngine = this.projects.Any(project => project.Engine == this.SelectedEngine.Appid);

            if (engineData != null && isAvailableProjectBySelectEngine)
            {
                var enginePath = engineData.Path;

                if (enginePath != null)
                {
                    var iconPathEngine = Path.Combine(enginePath, this.SelectedEngine.Bin);

                    var runProjectListViewItem = new ListViewItem(MenuItems.itmRunProject, iconPathEngine, this.listViewGroupTools)
                    {
                        Tag = this.runProjectId,
                    };

                    this.listView.Items.Add(runProjectListViewItem);
                }
            }
        }

        private void UpdateAddonsList()
        {
            var removeItem = new List<ListViewItem>();

            foreach (ListViewItem item in this.listViewGroupAddons.Items)
            {
                removeItem.Add(item);
            }

            removeItem.ForEach(item => this.listView.Items.Remove(item));

            if (this.SelectedEngine == null)
            {
                return;
            }

            var pathSelectedEngine = SteamPathsUtil.GetSteamAppManifestDataById(this.SelectedEngine.Appid)?.Path;
            var addonsSelectedEngine = this.dataProvider.Addons.Where(a => a.Engines.Contains(this.SelectedEngine.Appid));

            var pairPathIconTools = addonsSelectedEngine
                .Where(addon =>
                {
                    var path = addon.Bin;

                    return File.Exists(path);
                })
                .Select(addons =>
                {
                    var keyByPath = addons.Bin;
                    var icon = Icon.ExtractAssociatedIcon(keyByPath);

                    return (keyByPath, icon);
                });

            foreach (var (keyByPath, icon) in pairPathIconTools)
            {
                this.listView.SmallImageList.Images.Add(keyByPath, icon);
                this.listView.LargeImageList.Images.Add(keyByPath, icon);
            }

            var itemsAddons = addonsSelectedEngine.Select(addons =>
            {
                string keyByPath = null;

                if (pathSelectedEngine != null)
                {
                    keyByPath = Path.Combine(pathSelectedEngine, addons.Bin);
                }

                return keyByPath == null
                    ? new ListViewItem(addons.Name, this.listViewGroupAddons)
                    : new ListViewItem(addons.Name, keyByPath, this.listViewGroupAddons);
            }).ToArray();

            this.listView.Items.AddRange(itemsAddons);
        }

        private void UpdateInfoNavigationBar()
        {
            var countAvailableEngines = this.dataProvider.Engines.Length;
            var countAvailableProjects = this.dataProvider.Projects.Length;
            var countAvailableAddons = this.dataProvider.Addons.Length;

            this.toolStripStatusLabelEngines.Text = MenuNavbar.menuStrEngines + $"{countAvailableEngines}";
            this.toolStripStatusLabelAddons.Text = $"{MenuNavbar.menuStrAddons}{countAvailableAddons}";
        }

        private void UpdateLogInStatus()
        {
            var steamData = SteamManager.SteamData;

            if (steamData.SteamPid != 0)
            {
                //this.toolStripStatusLabelSteam.Image = Resources.checked_16;
                this.toolStripStatusLabelSteam.Text = MenuNavbar.menuStrSteam + MenuNavbar.menuStrOnline;
                this.toolStripStatusLabelSteam.ToolTipText = MenuNavbar.menuStrSteam + $"PID: {steamData.SteamPid}";
            }
            else
            {
                //this.toolStripStatusLabelSteam.Image = Resources.cancel_16;
                this.toolStripStatusLabelSteam.ToolTipText = string.Empty;
                this.toolStripStatusLabelSteam.Text = MenuNavbar.menuStrSteam + MenuNavbar.menuStrOffline;
            }

            this.toolStripStatusLabelLogin.Text = steamData.UserNameSteam != null
                ? MenuNavbar.menuStrLogin + $"{steamData.UserNameSteam}"
                : MenuNavbar.menuStrLogin + MenuNavbar.menuStrNone;
        }

        private void Button_Launch_Click(object sender, EventArgs e) => this.OpenSettings();

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selectItem = this.listView.SelectedItems[0];

            var selectItemText = selectItem.Text;
            EnvUtils.PrepareVProject($"{this.SelectedProject?.Path ?? string.Empty}");
            EnvUtils.PrepareSFMData($"{this.SelectedProject?.Path ?? string.Empty}");

            if (selectItem.Group == this.listViewGroupTools)
            {
                if (this.runProjectId.Equals(selectItem.Tag) && this.SelectedProject != null)
                {
                    var pathEngineBin = Path.Combine(SteamPathsUtil.GetSteamAppManifestDataById(this.SelectedEngine.Appid).Path, this.SelectedEngine.Bin);

                    Process.Start(pathEngineBin, $"-steam -game \"{this.SelectedProject?.Path ?? string.Empty}\" {this.SelectedProject.Args}");
                }

                var selectedTool = this.SelectedEngine.Tools.FirstOrDefault(tool => tool.Name == selectItemText);
                if (selectedTool != null)
                {
                    var selectedEnginePath = SteamPathsUtil.GetSteamAppManifestDataById(this.SelectedEngine.Appid)?.Path;

                    if (selectedEnginePath != null)
                    {
                        var toolPath = Path.Combine(selectedEnginePath, selectedTool.Bin);

                        if (File.Exists(toolPath))
                        {
                            var finalArg = $"-steam {selectedTool.Args}";

                            if (!finalArg.Contains("-game"))
                            {
                                finalArg += $"-game \"{this.SelectedProject?.Path ?? string.Empty}\"";
                            }

                            Process.Start(toolPath, finalArg);
                        }
                        else
                        {
                            MessageBox.Show($"\"{selectedTool.Name}\" no found.\n{toolPath}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        var dialogResult = MessageBox.Show($"\"{this.SelectedEngine.Name}\" with app id \"{this.SelectedEngine.Appid}\" not installed. Do you want to install it?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Process.Start($"steam://install/{this.SelectedEngine.Appid}");
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            MessageBox.Show($"Installation of \"{this.SelectedEngine.Name}\" with app id \"{this.SelectedEngine.Appid}\" canceled.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            else if (selectItem.Group == this.listViewGroupAddons)
            {
                var selectedAddons = this.dataProvider.Addons.FirstOrDefault(addon => addon.Name == selectItemText);
                if (selectedAddons != null)
                {
                    var addonPath = Path.Combine(selectedAddons.Bin);

                    if (File.Exists(addonPath))
                    {
                        Process.Start(addonPath, selectedAddons.Args);
                    }
                    else
                    {
                        MessageBox.Show($"\"{selectedAddons.Name}\" no found.\n{addonPath}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (selectItemText == MenuItems.itmOpenSettings)
            {
                this.OpenSettings();
            }
            else if (selectItemText == MenuItems.itmEditConfigurations)
            {
                var frmProfiles = new FormProjects();
                if (frmProfiles.ShowDialog() == DialogResult.OK)
                {
                    this.UpdateFormData();
                }
            }
            else if (selectItemText == MenuItems.itmEditPlugins)
            {
                var frmPlugins = new FormAddons();
                if (frmPlugins.ShowDialog() == DialogResult.OK)
                {
                    this.UpdateFormData();
                }
            }
            else if (selectItemText == MenuItems.itmAbout)
            {
                var frmAbout = new FormAbout();
                frmAbout.ShowDialog();
            }
            else if (selectItemText == MenuItems.itmGitHubLink)
            {
                Process.Start("https://github.com/EpicMorg/UniversalValveToolbox");
            }
            else if (selectItemText == MenuItems.itmGitHubReport)
            {
                Process.Start("https://github.com/EpicMorg/UniversalValveToolbox/issues/new/choose");
            }
        }

        private void OpenSettings()
        {
            var dataManager = new DataProvider();
            var settingsDto = dataManager.Settings;
            var languageProvider = new LanguageProvider();

            var settingsModel = new SettingsViewModel(settingsDto, languageProvider);

            var frmSettings = new FormSettings(settingsModel);

            if (frmSettings.ShowDialog() == DialogResult.OK)
            {
                dataManager.Settings = settingsDto;
                Application.Restart();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) => this.SaveLastSelectedProject();
    }
}
