using System;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Model.ViewModel;
using UniversalValveToolbox.Model.Dto;
using kasthack.binding.wf;
using System.Linq;
using System.Collections.Generic;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox {
    public partial class FormAddons : Form {
        private DataProvider dataProvider = new DataProvider();

        private FormAddonViewModel model;

        private AddonDtoModel[] arrayAddon;

        public FormAddons() {
            InitializeComponent();

            model = new FormAddonViewModel(dataProvider.Addons, dataProvider.Engines);

            UpdateAddonsComboBox();
            InitEnginesListView();

            comboBox_Addon.Bind(a => a.SelectedIndex, model, a => a.SelectAddonIndex);

            textBoxName.Bind(a => a.Text, model, a => a.SelectAddon.Name);
            textBoxPath.Bind(a => a.Text, model, a => a.SelectAddon.Bin);
            textBoxArgs.Bind(a => a.Text, model, a => a.SelectAddon.Args);

            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            UpdateEnginesListView();
        }


        private void EngineListView_ItemChecked(object sender, System.Windows.Forms.ItemCheckedEventArgs e) {
            var listCheckedIndex = new List<int>();

            if (engineListView.Items == null)
                return;

            foreach (ListViewItem item in engineListView.Items) {
                if (item?.Checked ?? false) {
                    listCheckedIndex.Add(item.Index);
                }
            }

            model.ArraySelectAddonIndex = listCheckedIndex.ToArray();
        }


        private void InitEnginesListView() {
            var checkedEngineListItem = dataProvider.Engines.Select(engine => {
                var item = new ListViewItem(engine.Name);
                item.Checked = model.SelectAddon.Engines.Contains(engine.Appid);
                return item;
            }).ToArray();

            engineListView.Items.Clear();
            engineListView.Items.AddRange(checkedEngineListItem);
        }

        private void UpdateAddonsComboBox() {
            comboBox_Addon.Items.Clear();
            comboBox_Addon.Items.AddRange(model.Addons);
            comboBox_Addon.SelectedIndex = 0;
        }

        private void UpdateEnginesListView() {
            var indexs = model.ArraySelectAddonIndex;

            for (var i = 0; i < model.Engines.Length; i++) {
                engineListView.Items[i].Checked = indexs.Contains(i);
            }
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

        private void FormAddons_Load(object sender, EventArgs e) {

        }

        private void comboBox_Addon_SelectedIndexChanged(object sender, EventArgs e) {
            model.SelectAddonIndex = comboBox_Addon.SelectedIndex;
        }

        private void engineListView_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void Remove() {
            var newAddonList = new List<AddonDtoModel>(model.Addons);
            newAddonList.RemoveAt(model.SelectAddonIndex);

            model.Addons = newAddonList.ToArray();

            UpdateAddonsComboBox();
        }

        private void New() {
            var newAddon = new AddonDtoModel();
            newAddon.Name = "<new addon>";

            var newAddonList = new List<AddonDtoModel>(model.Addons);
            newAddonList.Insert(0, newAddon);

            model.Addons = newAddonList.ToArray();

            UpdateAddonsComboBox();
        }

        private void Save() {


            JsonFileUtil.SaveValues(DataProvider.AddonsPath, "json", model.Addons.ToList());
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            Remove();
        }

        private void buttonNew_Click(object sender, EventArgs e) {
            New();
        }

        private void buttonApply_Click(object sender, EventArgs e) {
            Save();
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            Save();

            Close();
        }
    }
}
