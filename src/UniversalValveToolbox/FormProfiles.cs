using System;
using System.Windows.Forms;

namespace UniversalValveToolbox {
    public partial class FormProfiles : Form {
        public FormProfiles() {
            InitializeComponent();
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
