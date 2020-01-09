using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalValveToolbox
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            FillBaseMenuItems();


        }
 
        private void FormMain_Load(object sender, EventArgs e)
        {
       
        }

        private void FillBaseMenuItems() {
            ListViewGroup listViewGroupSettings = new ListViewGroup(Properties.str.strSettings);
            ListViewGroup listViewGroupWebLinks = new ListViewGroup(Properties.str.strWebLinks);
       
            ListViewItem listViewItemSettings = new ListViewItem(Properties.str.strSettings, 2);
            ListViewItem listViewItemEditConfigurations = new ListViewItem(Properties.str.strEditConfigurations, 3);
            ListViewItem listViewItemGitHubLink = new ListViewItem(Properties.str.strGitHubLink, 0);

            listViewGroupSettings.Header = Properties.str.strSettings;
            listViewGroupSettings.Name = "ListViewGroupSettings";

            listViewGroupWebLinks.Header = Properties.str.strWebLinks;
            listViewGroupWebLinks.Name = "ListViewGroupUrls";
           
            listViewItemSettings.Group = listViewGroupSettings;
            listViewItemEditConfigurations.Group = listViewGroupSettings;

            listViewItemGitHubLink.Group = listViewGroupWebLinks;

            listView.Groups.AddRange(new ListViewGroup[] { listViewGroupSettings, listViewGroupWebLinks });
            listView.Items.AddRange(new ListViewItem[] { listViewItemSettings, listViewItemEditConfigurations, listViewItemGitHubLink });
        }

        private void button_Launch_Click(object sender, EventArgs e)
        {
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
                    
                    if (selectedClient == Properties.str.strSettings) { 
                        #if DEBUG
                        MessageBox.Show(selectedClient, Properties.str.strInfo, MessageBoxButtons.OK,MessageBoxIcon.Information);
                        #endif
                        var frmSettings = new FormSettings();
                        frmSettings.ShowDialog();
                    } else if (selectedClient == Properties.str.strEditConfigurations) {
                        #if DEBUG
                        MessageBox.Show(selectedClient, Properties.str.strInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        #endif
                        var frmSettings = new FormSettings();
                        frmSettings.ShowDialog();
                    } else if (selectedClient == Properties.str.strGitHubLink) {
                        #if DEBUG
                        MessageBox.Show(selectedClient, Properties.str.strInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        #endif
                        Process.Start("https://github.com/EpicMorg/UniversalValveToolbox");
                    }
                    return;
                }
            }
        }
    }
}
