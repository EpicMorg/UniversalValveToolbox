using System;
using System.Windows.Forms;

namespace UniversalValveToolbox {
    public partial class FormProfiles : Form {
        public FormProfiles() {
            InitializeComponent();
        }

        private void FormEditProfile_Load(object sender, EventArgs e) {

        }

        private void buttonNew_Click(object sender, EventArgs e) {
            var frmProfilePropertie = new FormProfilePropertie();
            frmProfilePropertie.ShowDialog();
        }

        private void buttonEdit_Click(object sender, EventArgs e) {
            var frmProfilePropertie = new FormProfilePropertie();
            frmProfilePropertie.ShowDialog();
        }
    }
}
