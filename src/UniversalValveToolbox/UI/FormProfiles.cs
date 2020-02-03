using kasthack.binding.wf;
using System;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Model.ViewModel;

namespace UniversalValveToolbox {
    public partial class FormProfiles : Form {
        private DataProvider dataProvider = new DataProvider();

        private FormProjectViewModel model = new FormProjectViewModel();

        public FormProfiles() {
            InitializeComponent();

            comboBox_Mod.Items.Clear();
            comboBox_Mod.Items.AddRange(dataProvider.Projects);
            //textBox1.Text = "1";

            //textBox1.Bind(a => a.Name, model, a => a.SelectProject.Name);
        }

        private void FormEditProfile_Load(object sender, EventArgs e) {

        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e) {
            string folderpath = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK) {
                folderpath = fbd.SelectedPath;
            }

            if (folderpath != "") {
                textBoxPath.Text = folderpath;
            }
        }
    }
}
