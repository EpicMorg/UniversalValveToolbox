﻿using System;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Model.ViewModel;
using UniversalValveToolbox.Model.Dto;
using kasthack.binding.wf;
using System.Linq;
using System.Collections.Generic;
using UniversalValveToolbox.Utils;
using System.Globalization;
using System.Collections;
using Steamworks;

namespace UniversalValveToolbox {
    public partial class FormAddons : Form {
        private bool needRestart;
        private bool isEnableListBoxCheckListener = false;

        private DataProvider dataProvider = new DataProvider();

        private FormAddonViewModel model;


        public FormAddons() {
            InitializeComponent();

            var categories = Properties.translations.MenuCategories
                .ResourceManager
                .GetResourceSet(CultureInfo.CurrentCulture, false, true)
                .Cast<DictionaryEntry>()
                .ToArray();

            model = new FormAddonViewModel(dataProvider.Addons, dataProvider.Engines.Where(engine => isSteamAppInstall(engine.Appid)).ToArray(), categories);

            UpdateAddonsComboBox();
            UpdateAddonCategoryComboBox();
            UpdateEngineCheckedListView();

            comboBox_Addon.Bind(a => a.SelectedIndex, model, a => a.SelectAddonIndex);
            comboBoxCategory.Bind(a => a.SelectedIndex, model, a => a.SelectCategoryIndex);

            textBoxName.Bind(a => a.Text, model, a => a.SelectAddon.Name);
            textBoxPath.Bind(a => a.Text, model, a => a.SelectAddon.Bin);
            textBoxArgs.Bind(a => a.Text, model, a => a.SelectAddon.Args);

            model.PropertyChanged += Model_PropertyChanged;

            this.engineCheckedListBox.ItemCheck += EngineCheckedListBox_ItemCheck;
        }

        private void EngineCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (isEnableListBoxCheckListener) {
                List<EngineDtoModel> checkedItems = new List<EngineDtoModel>();
                foreach (var item in engineCheckedListBox.CheckedItems)
                    checkedItems.Add((EngineDtoModel)item);

                if (e.NewValue == CheckState.Checked)
                    checkedItems.Add((EngineDtoModel)engineCheckedListBox.Items[e.Index]);
                else
                    checkedItems.Remove((EngineDtoModel)engineCheckedListBox.Items[e.Index]);


                model.SelectAddon.Engines = checkedItems.Select(engine => engine.Appid).ToArray();
            }
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(model.SelectAddon)) {
                UpdateEngineCheckedListView();
            }
        }

        private void UpdateEngineCheckedListView() {
            isEnableListBoxCheckListener = false;

            engineCheckedListBox.Items.Clear();
            engineCheckedListBox.Items.AddRange(model.Engines);

            if (model.SelectAddon != null) {
                for (var i = 0; i < model.Engines.Length; i++) {
                    var engine = model.Engines[i];

                    if (model.SelectAddon.Engines.Contains(engine.Appid)) {
                        engineCheckedListBox.SetItemChecked(i, true);
                    }
                }
            }
            else {
                for (var i = 0; i < model.Engines.Length; i++) {
                    engineCheckedListBox.SetItemChecked(i, false);
                }
            }

            isEnableListBoxCheckListener = true;
        }

        private void UpdateAddonsComboBox() {
            if (model.Addons.Length == 0) {
                New();
            }
            else {
                comboBox_Addon.Items.Clear();
                comboBox_Addon.Items.AddRange(model.Addons);

                comboBox_Addon.SelectedIndex = 0;
            }
        }

        private bool isSteamAppInstall(int appId) {
            var isInstall = SteamApps.IsAppInstalled(appId);
            return isInstall;
        }

        private void UpdateAddonCategoryComboBox() {
            comboBoxCategory.Items.Clear();
            comboBoxCategory.Items.AddRange(model.Categories.Select(c => c.Value).ToArray());

            var index = Array.IndexOf(model.Categories.Select(c => c.Key).ToArray(), model.SelectAddon.Category);
            comboBoxCategory.SelectedIndex = index;
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog {
                InitialDirectory = @"C:\",
                DefaultExt = "exe",
                Filter = "Exe file (*.exe) | *.exe",
                Multiselect = false,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK) {
                textBoxPath.Text = dialog.FileName;
            }
        }

        private void FormAddons_Load(object sender, EventArgs e) {

        }

        private void comboBox_Addon_SelectedIndexChanged(object sender, EventArgs e) {
            model.SelectAddonIndex = comboBox_Addon.SelectedIndex;
        }

        private void Remove() {
            var newAddonList = new List<AddonDtoModel>(model.Addons);
            newAddonList.RemoveAt(model.SelectAddonIndex);

            model.Addons = newAddonList.ToArray();

            UpdateAddonsComboBox();
        }

        private void New() {
            var newAddon = CreateNewEmptyAddon();

            var newAddonList = new List<AddonDtoModel>(model.Addons);
            newAddonList.Insert(0, newAddon);

            model.Addons = newAddonList.ToArray();

            UpdateAddonsComboBox();
        }

        private AddonDtoModel CreateNewEmptyAddon() {
            var newAddon = new AddonDtoModel();
            newAddon.Name = Properties.translations.VarStrings.strNewAddon;

            return newAddon;
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
            needRestart = true;

            Save();
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            Save();

            Close();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
