using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UniversalValveToolbox {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
            FillBaseMenuItems();
            Text = VersionHelper.AssemblyTitle + VersionHelper.AssemblyVersion;
            comboBoxEngine.SelectedIndex = 0;
            comboBoxGameConfig.SelectedIndex = 0;
        }

        private void FormMain_Load(object sender, EventArgs e) {

        }

        public void FillBaseMenuItems() {
            #region static content, do not edit
            //creating groups (categores)
            ListViewGroup listViewGroupAddons = new ListViewGroup(Properties.translations.MenuCategories.catAddons);
            ListViewGroup listViewGroupCompileDecpmpile = new ListViewGroup(Properties.translations.MenuCategories.catCompileDecpmpile);
            ListViewGroup listViewGroupContent = new ListViewGroup(Properties.translations.MenuCategories.catContent);
            ListViewGroup listViewGroupDocs = new ListViewGroup(Properties.translations.MenuCategories.catDocs);
            ListViewGroup listViewGroupLandscape = new ListViewGroup(Properties.translations.MenuCategories.catLandscape);
            ListViewGroup listViewGroupMisc = new ListViewGroup(Properties.translations.MenuCategories.catMisc);
            ListViewGroup listViewGroupSettings = new ListViewGroup(Properties.translations.MenuCategories.catSettings);
            ListViewGroup listViewGroupSupport = new ListViewGroup(Properties.translations.MenuCategories.catSupport);
            ListViewGroup listViewGroupTextures = new ListViewGroup(Properties.translations.MenuCategories.catTextures);
            ListViewGroup listViewGroupTools = new ListViewGroup(Properties.translations.MenuCategories.catTools);
            ListViewGroup listViewGroupUtils = new ListViewGroup(Properties.translations.MenuCategories.catUtils);
            ListViewGroup listViewGroupWebLinks = new ListViewGroup(Properties.translations.MenuCategories.catWebLinks);

            //initialise some categories in menu
            listViewGroupAddons.Header = Properties.translations.MenuCategories.catAddons;
            listViewGroupCompileDecpmpile.Header = Properties.translations.MenuCategories.catCompileDecpmpile;
            listViewGroupContent.Header = Properties.translations.MenuCategories.catContent;
            listViewGroupDocs.Header = Properties.translations.MenuCategories.catDocs;
            listViewGroupLandscape.Header = Properties.translations.MenuCategories.catLandscape;
            listViewGroupMisc.Header = Properties.translations.MenuCategories.catMisc;
            listViewGroupSettings.Header = Properties.translations.MenuCategories.catSettings;
            listViewGroupSupport.Header = Properties.translations.MenuCategories.catSupport;
            listViewGroupTextures.Header = Properties.translations.MenuCategories.catTextures;
            listViewGroupTools.Header = Properties.translations.MenuCategories.catTools;
            listViewGroupUtils.Header = Properties.translations.MenuCategories.catUtils;
            listViewGroupWebLinks.Header = Properties.translations.MenuCategories.catWebLinks;

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
                listViewGroupAddons,
                listViewGroupCompileDecpmpile,
                listViewGroupContent,
                listViewGroupDocs,
                listViewGroupLandscape,
                listViewGroupMisc,
                listViewGroupSettings,
                listViewGroupSupport,
                listViewGroupTextures,
                listViewGroupTools,
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



        private void button_Launch_Click(object sender, EventArgs e) {
            var frmSettings = new FormSettings();
            frmSettings.ShowDialog();
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e) {

            /*
             * Part of code by STAM. Sorry.
             * 
             * 
             *                       = *
             *                        +#%.                     
             *                        @*.+@#####+              
             *                       =%     % +
             *                      :#.    *@                  
             *         .%##        .#-    -#.     +##-         
             *       .%##*.        %+    .#*       -###-       
             *     .%##*          *###+-.=%          -###-     
             *     +##+.         -#.   .=#.           -##@     
             *      .%##+.       +@    .#-          -###.      
             *        .%##*       =%.-%#*         -###-        
             *          .* -       .#=.            .+-          
             *                    ==
             *                   *#                            
             *                  .#-                            
             *                 .@+
             *                -##*                             
             *                .= *
             *                
             *                           Nope. I'm really sorry.
             */
            for (int i = 0; i < listView.Items.Count; i++) {
                var rectangle = listView.GetItemRect(i);
                if (rectangle.Contains(e.Location)) {
                    var selectedClient = listView.SelectedItems[0].Text;
                    if (selectedClient == Properties.translations.MenuItems.itmOpenSettings) {
                        var frmSettings = new FormSettings();
                        frmSettings.ShowDialog();
                    } else if (selectedClient == Properties.translations.MenuItems.itmEditConfigurations) {
                        var frmProfiles = new FormProfiles();
                        frmProfiles.ShowDialog();
                    } else if (selectedClient == Properties.translations.MenuItems.itmEditPlugins) {
                        var frmPlugins = new FormAddons();
                        frmPlugins.ShowDialog();
                    } else if (selectedClient == Properties.translations.MenuItems.itmAbout) {
                        var frmAbout = new FormAbout();
                        frmAbout.ShowDialog();
                    } else if (selectedClient == Properties.translations.MenuItems.itmGitHubLink) {
                        Process.Start("https://github.com/EpicMorg/UniversalValveToolbox");
                    }
                    return;
                }
            }
        }
    }
}
