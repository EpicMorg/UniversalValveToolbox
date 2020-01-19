using kasthack.binding.wf;
using System;
using System.Windows.Forms;
using UniversalValveToolbox.Util.Dto;

namespace UniversalValveToolbox {
    public partial class FormSettings : Form {
        public FormSettings(SettingsViewModel settings) {
            InitializeComponent();

            comboBoxLang.SelectedIndex = 0;
            comboBoxTheme.SelectedIndex = 0;

            comboBoxLang.Bind(a => a.DataSource, settings, a => a.Languages);
            comboBoxLang.Bind(a => a.SelectedIndex, settings, a => a.SelectedLanguageIndex);

            //this.Bind(a => a.Text, settings, a => a.Language);

            //^
            //this.DataBindings.Add(new Binding(nameof(this.Text), settings, nameof(settings.Language), false, DataSourceUpdateMode.OnPropertyChanged));

        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void FormSettings_Load(object sender, EventArgs e) {

        }
    }
}
