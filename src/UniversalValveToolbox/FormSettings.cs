using System; 
using System.Windows.Forms;

namespace UniversalValveToolbox {
    public partial class FormSettings : Form {
        public FormSettings() {
            InitializeComponent();
            comboBoxLang.SelectedIndex = 0;
            comboBoxTheme.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void FormSettings_Load(object sender, EventArgs e) {

        }
    }
}
