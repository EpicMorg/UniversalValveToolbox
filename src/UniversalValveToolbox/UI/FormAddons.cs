namespace UniversalValveToolbox
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    using EpicMorg.SteamPathsLib;
    using kasthack.binding.wf;
    using UniversalValveToolbox.Model.Dto;
    using UniversalValveToolbox.Model.Provider;
    using UniversalValveToolbox.Model.ViewModel;
    using UniversalValveToolbox.Utils;

    public partial class FormAddons : Form
    {
        private bool needRestart;
        private bool isEnableListBoxCheckListener = false;

        private readonly DataProvider dataProvider = new DataProvider();

        private readonly FormAddonViewModel model;

        public FormAddons()
        {
            this.InitializeComponent();

            var categories = Properties.translations.MenuCategories
                .ResourceManager
                .GetResourceSet(CultureInfo.CurrentCulture, false, true)
                .Cast<DictionaryEntry>()
                .ToArray();

            this.model = new FormAddonViewModel(this.dataProvider.Addons, this.dataProvider.Engines.Where(engine => SteamPathsUtil.GetSteamAppDataById(engine.Appid) != null).ToArray(), categories);

            this.UpdateAddonsComboBox();
            this.UpdateAddonCategoryComboBox();
            this.UpdateEngineCheckedListView();

            this.comboBox_Addon.Bind(a => a.SelectedIndex, this.model, a => a.SelectAddonIndex);
            this.comboBoxCategory.Bind(a => a.SelectedIndex, this.model, a => a.SelectCategoryIndex);

            this.textBoxName.Bind(a => a.Text, this.model, a => a.SelectAddon.Name);
            this.textBoxPath.Bind(a => a.Text, this.model, a => a.SelectAddon.Bin);
            this.textBoxArgs.Bind(a => a.Text, this.model, a => a.SelectAddon.Args);

            this.model.PropertyChanged += this.Model_PropertyChanged;

            this.engineCheckedListBox.ItemCheck += this.EngineCheckedListBox_ItemCheck;
        }

        private void EngineCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.isEnableListBoxCheckListener)
            {
                var checkedItems = new List<EngineDtoModel>();
                foreach (var item in this.engineCheckedListBox.CheckedItems)
                {
                    checkedItems.Add((EngineDtoModel)item);
                }

                if (e.NewValue == CheckState.Checked)
                {
                    checkedItems.Add((EngineDtoModel)this.engineCheckedListBox.Items[e.Index]);
                }
                else
                {
                    checkedItems.Remove((EngineDtoModel)this.engineCheckedListBox.Items[e.Index]);
                }

                this.model.SelectAddon.Engines = checkedItems.Select(engine => engine.Appid).ToArray();
            }
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.model.SelectAddon))
            {
                this.UpdateEngineCheckedListView();
            }
        }

        private void UpdateEngineCheckedListView()
        {
            this.isEnableListBoxCheckListener = false;

            this.engineCheckedListBox.Items.Clear();
            this.engineCheckedListBox.Items.AddRange(this.model.Engines);

            if (this.model.SelectAddon != null)
            {
                for (var i = 0; i < this.model.Engines.Length; i++)
                {
                    var engine = this.model.Engines[i];

                    if (this.model.SelectAddon.Engines.Contains(engine.Appid))
                    {
                        this.engineCheckedListBox.SetItemChecked(i, true);
                    }
                }
            }
            else
            {
                for (var i = 0; i < this.model.Engines.Length; i++)
                {
                    this.engineCheckedListBox.SetItemChecked(i, false);
                }
            }

            this.isEnableListBoxCheckListener = true;
        }

        private void UpdateAddonsComboBox()
        {
            if (this.model.Addons.Length == 0)
            {
                this.New();
            }
            else
            {
                this.comboBox_Addon.Items.Clear();
                this.comboBox_Addon.Items.AddRange(this.model.Addons);

                this.comboBox_Addon.SelectedIndex = 0;
            }
        }

        private void UpdateAddonCategoryComboBox()
        {
            this.comboBoxCategory.Items.Clear();
            this.comboBoxCategory.Items.AddRange(this.model.Categories.Select(c => c.Value).ToArray());

            var index = Array.IndexOf(this.model.Categories.Select(c => c.Key).ToArray(), this.model.SelectAddon.Category);
            this.comboBoxCategory.SelectedIndex = index;
        }

        private void ButtonCancel_Click(object sender, EventArgs e) => this.Close();

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                DefaultExt = "exe",
                Filter = "Exe file (*.exe) | *.exe",
                Multiselect = false,
                RestoreDirectory = true,
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxPath.Text = dialog.FileName;
            }
        }

        private void FormAddons_Load(object sender, EventArgs e)
        {
        }

        private void ComboBox_Addon_SelectedIndexChanged(object sender, EventArgs e) => this.model.SelectAddonIndex = this.comboBox_Addon.SelectedIndex;

        private void Remove()
        {
            var newAddonList = new List<AddonDtoModel>(this.model.Addons);
            newAddonList.RemoveAt(this.model.SelectAddonIndex);

            this.model.Addons = newAddonList.ToArray();

            this.UpdateAddonsComboBox();
        }

        private void New()
        {
            var newAddon = this.CreateNewEmptyAddon();

            var newAddonList = new List<AddonDtoModel>(this.model.Addons);
            newAddonList.Insert(0, newAddon);

            this.model.Addons = newAddonList.ToArray();

            this.UpdateAddonsComboBox();
        }

        private AddonDtoModel CreateNewEmptyAddon()
        {
            var newAddon = new AddonDtoModel
            {
                Name = Properties.translations.VarStrings.strNewAddon,
            };

            return newAddon;
        }

        private void Save() => JsonFileUtil.SaveValues(DataProvider.AddonsPath, "json", this.model.Addons.ToList());

        private void ButtonRemove_Click(object sender, EventArgs e) => this.Remove();

        private void ButtonNew_Click(object sender, EventArgs e) => this.New();

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            this.needRestart = true;

            this.Save();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.Save();

            this.Close();
        }

        private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
