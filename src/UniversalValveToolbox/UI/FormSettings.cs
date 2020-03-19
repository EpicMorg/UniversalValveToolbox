using kasthack.binding.wf;
using System;
using System.Threading;
using System.Windows.Forms;
using UniversalValveToolbox.Model.ViewModel;

namespace UniversalValveToolbox {
    public partial class FormSettings : Form {
        public FormSettings(SettingsViewModel settings) {
            InitializeComponent();

            comboBoxLang.Items.Clear();
            comboBoxLang.Items.AddRange(settings.Languages);
            comboBoxLang.Bind(a => a.SelectedIndex, settings, a => a.SelectedLanguageIndex);
 
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void FormSettings_Load(object sender, EventArgs e) {
            labelTranslationAthor.Text = Properties.translations.LangDict.strLangAuthor;
        }
    }
}
