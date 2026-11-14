using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Dto;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Model.ViewModel;
using UniversalValveToolbox.Steam;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox
{
    public partial class FormMain : Form
    {
        private readonly string RUN_PROJECT_ID = "RUN_PROJECT_ID";

        // icons of tools/addons by exe path, extracted once per run
        private static readonly Dictionary<string, Icon> iconCache = new Dictionary<string, Icon>(StringComparer.OrdinalIgnoreCase);

        private EngineDtoModel[] Engines = new EngineDtoModel[0];
        private ProjectDtoModel[] Projects = new ProjectDtoModel[0];
        private ProjectDtoModel[] AvailableProjects = new ProjectDtoModel[0];
        private AddonDtoModel[] Addons = new AddonDtoModel[0];
        private SettingsDtoModel settings;

        private DataProvider dataProvider = new DataProvider();

        private ListViewGroup listViewGroupAddons;
        private ListViewGroup listViewGroupTools;

        // set while UpdateFormData fills the engine box, so SelectedIndexChanged does not rebuild everything again
        private bool suppressEngineChanged;

        private EngineDtoModel SelectedEngine =>
            comboBoxEngine.SelectedIndex >= 0 && comboBoxEngine.SelectedIndex < Engines.Length ? Engines[comboBoxEngine.SelectedIndex] : null;

        // by index in the list of the selected engine's projects (names may repeat across engines)
        private ProjectDtoModel SelectedProject =>
            comboBoxProjects.Enabled && comboBoxProjects.SelectedIndex >= 0 && comboBoxProjects.SelectedIndex < AvailableProjects.Length
                ? AvailableProjects[comboBoxProjects.SelectedIndex]
                : null;

        public FormMain()
        {
            InitializeComponent();
            FillBaseMenuItems();

            comboBoxEngine.SelectedIndexChanged += (s, e) =>
            {
                if (suppressEngineChanged)
                    return;
                UpdateProjectList();
                UpdateToolsList();
                UpdateAddonsList();
            };

            UpdateFormData();

            Text = VersionHelper.AssemblyTitle + VersionHelper.AssemblyVersion;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        /// <summary>Root folder of an engine in the current mode, or null when it is not installed.</summary>
        private string EnginePath(EngineDtoModel engine)
        {
            if (engine == null)
                return null;
            return settings.ToolboxMod switch
            {
                ToolboxMod.retail => SteamApi.AppInstallDir(engine.Appid),
                ToolboxMod.bundle => SteamApi.AppInstallDir(settings.BundleAppID),
                ToolboxMod.dev => settings.DevEnginePath,
                _ => null
            };
        }

        private void SaveLastSelectedProject()
        {
            var current = dataProvider.Settings;
            current.LastSelectedProject = SelectedProject?.Name;
            dataProvider.Settings = current;
        }

        /// <summary>Reloads settings, catalog, projects and addons and rebuilds the window in one pass.</summary>
        private void UpdateFormData()
        {
            settings = dataProvider.Settings;
            Projects = dataProvider.Projects;
            Addons = dataProvider.Addons;

            suppressEngineChanged = true;
            try
            {
                UpdateEngineList();

                // engine of the last used project, if it is in the list
                var lastProject = Projects.FirstOrDefault(project => project.Name == settings.LastSelectedProject);
                var lastEngineIndex = lastProject == null ? -1 : Array.FindIndex(Engines, engine => engine.Appid == lastProject.Engine);
                if (lastEngineIndex >= 0)
                    comboBoxEngine.SelectedIndex = lastEngineIndex;

                UpdateProjectList();
                if (lastProject != null)
                {
                    var lastProjectIndex = Array.IndexOf(AvailableProjects, lastProject);
                    if (lastProjectIndex >= 0)
                        comboBoxProjects.SelectedIndex = lastProjectIndex;
                }
            }
            finally
            {
                suppressEngineChanged = false;
            }

            UpdateToolsList();
            UpdateAddonsList();
            UpdateNavigationBar();
        }

        private void UpdateNavigationBar()
        {
            UpdateLogInStatus();
            UpdateInfoNavigationBar();
        }

        private void toolStripStatusLabelRefresh_Click(object sender, EventArgs e)
        {
            SteamApi.ClearCache(); // games may have been moved/uninstalled
            UpdateFormData();
        }

        public void FillBaseMenuItems()
        {
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

        private void UpdateEngineList()
        {
            Engines = GetAvailableEngines();

            comboBoxEngine.Items.Clear();
            comboBoxEngine.Enabled = Engines.Length != 0;
            if (Engines.Length != 0)
            {
                comboBoxEngine.Items.AddRange(Engines.Select(engine => engine.Name).ToArray());
                comboBoxEngine.SelectedIndex = 0;
            }
        }

        private EngineDtoModel[] GetAvailableEngines()
        {
            var catalog = dataProvider.Engines;
            switch (settings.ToolboxMod)
            {
                case ToolboxMod.dev:
                    // tools come from the catalog entry of BundleAppID; without one there is nothing to show
                    var template = catalog.FirstOrDefault(engine => engine.Appid == settings.BundleAppID);
                    return template == null ? new EngineDtoModel[0] : new[] {
                        new EngineDtoModel {
                            Appid = (int)settings.BundleAppID,
                            Name = settings.DevEngineName,
                            Bin = template.Bin,
                            AltBins = template.AltBins,
                            Tools = template.Tools
                        }
                    };
                case ToolboxMod.bundle:
                    return catalog.Where(engine => engine.Appid == settings.BundleAppID).ToArray();
                default:
                    return catalog.Where(engine => SteamApi.IsAppInstalled(engine.Appid)).ToArray();
            }
        }

        private void UpdateProjectList()
        {
            var engine = SelectedEngine;
            AvailableProjects = engine == null ? new ProjectDtoModel[0] : Projects.Where(project => project.Engine == engine.Appid).ToArray();

            comboBoxProjects.Items.Clear();
            comboBoxProjects.Enabled = AvailableProjects.Length != 0;
            if (AvailableProjects.Length != 0)
            {
                comboBoxProjects.Items.AddRange(AvailableProjects.Select(project => project.Name).ToArray());
                comboBoxProjects.SelectedIndex = 0;
            }
        }

        /// <summary>Adds the exe's icon to the image lists once; returns the image key, or null for a missing file.</summary>
        private string EnsureIcon(string exePath)
        {
            if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath))
                return null;

            if (!listView.SmallImageList.Images.ContainsKey(exePath))
            {
                if (!iconCache.TryGetValue(exePath, out var icon))
                {
                    try { icon = Icon.ExtractAssociatedIcon(exePath); }
                    catch (Exception) { icon = null; }
                    iconCache[exePath] = icon;
                }
                if (icon == null)
                    return null;
                listView.SmallImageList.Images.Add(exePath, icon);
                listView.LargeImageList.Images.Add(exePath, icon);
            }
            return exePath;
        }

        private void RemoveGroupItems(ListViewGroup group)
        {
            foreach (var item in group.Items.Cast<ListViewItem>().ToList())
                listView.Items.Remove(item);
        }

        private void UpdateToolsList()
        {
            listView.BeginUpdate();
            try
            {
                RemoveGroupItems(listViewGroupTools);

                var engine = SelectedEngine;
                var enginePath = EnginePath(engine);
                if (enginePath == null)
                    return; // not installed (or no folder yet): nothing to launch

                // only tools whose exe exists in this install (old and new builds of one app id differ)
                var items = engine.Tools
                    .Select(tool => (tool, path: Path.Combine(enginePath, tool.Bin)))
                    .Where(t => File.Exists(t.path))
                    .Select(t => new ListViewItem(t.tool.Name, EnsureIcon(t.path), listViewGroupTools))
                    .ToArray();
                listView.Items.AddRange(items);

                if (AvailableProjects.Length != 0)
                {
                    var iconKey = EnsureIcon(Path.Combine(enginePath, engine.ResolveBin(enginePath)));
                    listView.Items.Add(new ListViewItem(Properties.translations.MenuItems.itmRunProject, iconKey, listViewGroupTools) { Tag = RUN_PROJECT_ID });
                }
            }
            finally
            {
                listView.EndUpdate();
            }
        }

        private void UpdateAddonsList()
        {
            listView.BeginUpdate();
            try
            {
                RemoveGroupItems(listViewGroupAddons);

                var engine = SelectedEngine;
                if (engine == null)
                    return;

                // addons without engines are for every engine; like tools, only the ones that are installed
                var items = Addons
                    .Where(addon => addon.Engines == null || addon.Engines.Length == 0 || addon.Engines.Contains(engine.Appid))
                    .Select(addon => (addon, path: ResolveAddonPath(addon.Bin)))
                    .Where(a => File.Exists(a.path))
                    .Select(a => new ListViewItem(a.addon.Name, EnsureIcon(a.path), listViewGroupAddons))
                    .ToArray();
                listView.Items.AddRange(items);
            }
            finally
            {
                listView.EndUpdate();
            }
        }

        /// <summary>
        /// Addon exe path: environment variables expanded (%ProgramFiles%), relative paths are relative to the
        /// launcher folder (bundled tools live in tools\&lt;Name&gt;\).
        /// </summary>
        private static string ResolveAddonPath(string bin)
        {
            if (string.IsNullOrWhiteSpace(bin))
                return null;
            var path = Environment.ExpandEnvironmentVariables(bin.Trim());
            return Path.IsPathRooted(path) ? path : Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, path));
        }

        private void UpdateInfoNavigationBar()
        {
            toolStripStatusLabelEngines.Text = Properties.translations.MenuNavbar.menuStrEngines + $"{Engines.Length}";
            toolStripStatusLabelAddons.Text = Properties.translations.MenuNavbar.menuStrAddons + $"{Addons.Length}";
        }

        private void UpdateLogInStatus()
        {
            var steamData = SteamManager.SteamData;

            if (steamData.SteamPid != 0)
            {
                toolStripStatusLabelSteam.Image = Properties.Resources.checked_16;
                toolStripStatusLabelSteam.Text = Properties.translations.MenuNavbar.menuStrSteam + Properties.translations.MenuNavbar.menuStrOnline;
                toolStripStatusLabelSteam.ToolTipText = Properties.translations.MenuNavbar.menuStrSteam + $"PID: {steamData.SteamPid.ToString()}";
            }
            else
            {
                toolStripStatusLabelSteam.Image = Properties.Resources.cancel_16;
                toolStripStatusLabelSteam.ToolTipText = string.Empty;
                toolStripStatusLabelSteam.Text = Properties.translations.MenuNavbar.menuStrSteam + Properties.translations.MenuNavbar.menuStrOffline;
            }

            if (steamData.UserNameSteam != null)
            {
                toolStripStatusLabelLogin.Text = Properties.translations.MenuNavbar.menuStrLogin + $"{steamData.UserNameSteam}";
            }
            else
            {
                toolStripStatusLabelLogin.Text = Properties.translations.MenuNavbar.menuStrLogin + Properties.translations.MenuNavbar.menuStrNone;
            }
        }

        private void button_Launch_Click(object sender, EventArgs e)
        {
            this.OpenSettings();
        }

        /// <summary>Starts an engine program with the project's environment (process only + changed values to the user env).</summary>
        private void LaunchForProject(string exePath, string args, EngineDtoModel engine, string enginePath)
        {
            var env = EnvUtils.ForLaunch(SelectedProject, engine, enginePath);
            EnvUtils.ExportToUser(env);
            ShellUtils.Launch(exePath, args, env, enginePath);
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;
            var selectItem = listView.SelectedItems[0];
            var selectItemText = selectItem.Text;

            if (selectItem.Group == listViewGroupTools)
            {
                var engine = SelectedEngine;
                var enginePath = EnginePath(engine);
                var project = SelectedProject;

                if (RUN_PROJECT_ID.Equals(selectItem.Tag))
                {
                    if (enginePath != null && project != null)
                    {
                        // newer builds have their own exe (tf_win64.exe), older ones of the same app id still hl2.exe
                        var pathEngineBin = Path.Combine(enginePath, engine.ResolveBin(enginePath));
                        LaunchForProject(pathEngineBin, $"-steam -game \"{project.Path}\" {project.Args}", engine, enginePath);
                    }
                    return;
                }

                var selectedTool = engine?.Tools.FirstOrDefault(tool => tool.Name == selectItemText);
                if (selectedTool == null)
                    return;

                if (enginePath != null)
                {
                    var toolPath = Path.Combine(enginePath, selectedTool.Bin);

                    if (File.Exists(toolPath))
                    {
                        string finalArg = $"-steam {selectedTool.Args}";

                        // the selected project's folder, unless the tool names its own -game; nothing without a project
                        if (!finalArg.Contains("-game") && !string.IsNullOrEmpty(project?.Path))
                        {
                            finalArg += $" -game \"{project.Path}\"";
                        }
                        LaunchForProject(toolPath, finalArg, engine, enginePath);
                    }
                    else
                        MessageBox.Show($"\"{selectedTool.Name}\" {Properties.translations.MessageBoxes.msgTextNotFound}\n{toolPath}", Properties.translations.MessageBoxes.msgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show($"\"{engine.Name}\" {Properties.translations.MessageBoxes.msgTextWithAppID} \"{engine.Appid}\" {Properties.translations.MessageBoxes.msgTextNotInstalledInstall}", Properties.translations.MessageBoxes.msgWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ShellUtils.Start($"steam://install/{engine.Appid}");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show($"{Properties.translations.MessageBoxes.msgTextInstallationOf} \"{engine.Name}\" {Properties.translations.MessageBoxes.msgTextWithAppID} \"{engine.Appid}\" {Properties.translations.MessageBoxes.msgTextCancelled}", Properties.translations.MessageBoxes.msgInfo, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            else if (selectItem.Group == listViewGroupAddons)
            {
                var selectedAddons = Addons.FirstOrDefault(addon => addon.Name == selectItemText);
                if (selectedAddons != null)
                {
                    var addonPath = ResolveAddonPath(selectedAddons.Bin);

                    if (addonPath != null && File.Exists(addonPath))
                    {
                        var env = EnvUtils.ForLaunch(SelectedProject, SelectedEngine, EnginePath(SelectedEngine));
                        ShellUtils.Launch(addonPath, selectedAddons.Args, env, Path.GetDirectoryName(addonPath));
                    }
                    else
                        MessageBox.Show($"\"{selectedAddons.Name}\" {Properties.translations.MessageBoxes.msgTextNotFound}\n{addonPath}", Properties.translations.MessageBoxes.msgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (selectItemText == Properties.translations.MenuItems.itmOpenSettings)
            {
                this.OpenSettings();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmEditConfigurations)
            {
                var frmProfiles = new FormProjects();
                if (frmProfiles.ShowDialog() == DialogResult.OK)
                {
                    UpdateFormData();
                }
            }
            else if (selectItemText == Properties.translations.MenuItems.itmEditPlugins)
            {
                var frmPlugins = new FormAddons();
                if (frmPlugins.ShowDialog() == DialogResult.OK)
                {
                    UpdateFormData();
                }
            }
            else if (selectItemText == Properties.translations.MenuItems.itmAbout)
            {
                var frmAbout = new FormAbout();
                frmAbout.ShowDialog();
            }
            else if (selectItemText == Properties.translations.MenuItems.itmGitHubLink)
            {
                ShellUtils.Start("https://github.com/EpicMorg/UniversalValveToolbox");
            }
            else if (selectItemText == Properties.translations.MenuItems.itmGitHubReport)
            {
                ShellUtils.Start("https://github.com/EpicMorg/UniversalValveToolbox/issues/new/choose");
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLastSelectedProject();
            SteamApi.Shutdown();
        }
    }
}
