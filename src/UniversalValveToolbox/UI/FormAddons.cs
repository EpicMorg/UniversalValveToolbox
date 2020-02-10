using System;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Model.ViewModel;
using UniversalValveToolbox.Model.Dto;
using kasthack.binding.wf;
using System.Linq;

namespace UniversalValveToolbox {
    public partial class FormAddons : Form {
        private DataProvider dataProvider = new DataProvider();

        private FormAddonViewModel model;

        private AddonDtoModel[] arrayAddon;

        public FormAddons() {
            InitializeComponent();

            model = new FormAddonViewModel();
            model.SelectAddon = new AddonDtoModel();

            var addons = dataProvider.Addons;
            arrayAddon = new AddonDtoModel[addons.Length + 1];
            arrayAddon[0] = model.SelectAddon;
            Array.Copy(addons, 0, arrayAddon, 1, addons.Length);

            comboBox_Addon.Items.Clear();
            comboBox_Addon.Items.AddRange(arrayAddon);
            comboBox_Addon.SelectedIndex = 0;

            textBoxName.Bind(a => a.Text, model, a => a.SelectAddon.Name);
            textBoxPath.Bind(a => a.Text, model, a => a.SelectAddon.Bin);
            textBoxArgs.Bind(a => a.Text, model, a => a.SelectAddon.Args);

            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var availableEngines = dataProvider.Engines.Select(engine => {
                var item = new ListViewItem(engine.Name);
                item.Checked = model.SelectAddon.Engines.Contains(engine.Appid);
                return item;
            }).ToArray();


            engineListView.Items.Clear();
            engineListView.Items.AddRange(availableEngines);

            
            
            //dataGridViewEngines.Rows.add
            //var index = availableEngines.ToList().FindIndex(engine => engine.Appid == model.SelectProject.Engine);

            //comboBoxEngine.Items.Clear();
            //comboBoxEngine.Items.AddRange(availableEngines);

            //comboBoxEngine.SelectedIndex = index;
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

        private void FormAddons_Load(object sender, EventArgs e)
        {

        }

        private void comboBox_Addon_SelectedIndexChanged(object sender, EventArgs e) {
            var selectAddon = arrayAddon.First(addon => addon.Name == ((AddonDtoModel)comboBox_Addon.SelectedItem).Name);
            model.SelectAddon = selectAddon;
        }

        private void engineListView_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
