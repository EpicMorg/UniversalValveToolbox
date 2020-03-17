namespace UniversalValveToolbox
{
    using System;
    using System.Windows.Forms;
    using kasthack.binding.wf;
    using UniversalValveToolbox.Model.ViewModel;

    public partial class FormSettings : Form
    {
        public FormSettings(SettingsViewModel settings)
        {
            this.InitializeComponent();

            this.comboBoxLang.Items.Clear();
            this.comboBoxLang.Items.AddRange(settings.Languages);
            this.comboBoxLang.Bind(a => a.SelectedIndex, settings, a => a.SelectedLanguageIndex);
        }

        private void ButtonCancel_Click(object sender, EventArgs e) => this.Close();

        private void FormSettings_Load(object sender, EventArgs e)
        {
        }
    }
}
