namespace UniversalValveToolbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using EpicMorg.SteamPathsLib;
    using kasthack.binding.wf;
    using UniversalValveToolbox.Model.Dto;
    using UniversalValveToolbox.Model.Provider;
    using UniversalValveToolbox.Model.ViewModel;
    using UniversalValveToolbox.Utils;

    public partial class FormProjects : Form
    {
        private bool needRestart = false;

        private readonly DataProvider dataProvider = new DataProvider();

        private readonly FormProjectViewModel model;

        public FormProjects()
        {
            this.InitializeComponent();

            this.model = new FormProjectViewModel(this.dataProvider.Projects, this.dataProvider.Engines.Where(engine => SteamPathsUtil.GetSteamAppDataById(engine.Appid) != null).ToArray());

            this.UpdateComboBoxProject();
            this.UpdateComboBoxEngine();

            this.textBox1.Bind(a => a.Text, this.model, a => a.SelectProject.Name);
            this.textBoxPath.Bind(a => a.Text, this.model, a => a.SelectProject.Path);
            this.textBoxArgs.Bind(a => a.Text, this.model, a => a.SelectProject.Args);

            this.comboBox_Mod.Bind(a => a.SelectedIndex, this.model, a => a.SelectProjectIndex);
            this.comboBoxEngine.Bind(a => a.SelectedIndex, this.model, a => a.SelectEngineIndex);
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => this.UpdateComboBoxEngine();

        private void UpdateComboBoxProject()
        {
            if (this.model.Projects.Length == 0)
            {
                this.New();
            }
            else
            {
                this.comboBox_Mod.Items.Clear();
                this.comboBox_Mod.Items.AddRange(this.model.Projects);

                this.comboBox_Mod.SelectedIndex = this.model.SelectProjectIndex;
            }
        }

        private void UpdateComboBoxEngine()
        {
            this.comboBoxEngine.Items.Clear();
            this.comboBoxEngine.Items.AddRange(this.model.Engines);

            this.comboBoxEngine.SelectedIndex = this.model.SelectEngineIndex;
        }

        private void FormEditProfile_Load(object sender, EventArgs e)
        {
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (this.needRestart)
            {
                this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            var folderpath = string.Empty;
            var fbd = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.MyComputer,
            };
            var dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                folderpath = fbd.SelectedPath;
            }

            if (!string.IsNullOrEmpty(folderpath))
            {
                this.textBoxPath.Text = folderpath;
            }
        }

        private void ComboBoxEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.model.SelectProject != null && this.comboBoxEngine.SelectedItem != null)
            {
                this.model.SelectProject.Engine = ((EngineDtoModel)this.comboBoxEngine.SelectedItem).Appid;
            }
        }

        private void ComboBox_Mod_SelectedIndexChanged(object sender, EventArgs e) => this.model.SelectProjectIndex = this.comboBox_Mod.SelectedIndex;

        private void Remove()
        {
            var newProjectList = new List<ProjectDtoModel>(this.model.Projects);
            newProjectList.RemoveAt(this.model.SelectProjectIndex);

            this.model.Projects = newProjectList.ToArray();

            this.UpdateComboBoxProject();
        }

        private void New()
        {
            var newProject = this.CreateNewEmptyProject();

            var newProjectList = new List<ProjectDtoModel>(this.model.Projects);
            newProjectList.Insert(0, newProject);

            this.model.Projects = newProjectList.ToArray();

            this.UpdateComboBoxProject();
        }

        private ProjectDtoModel CreateNewEmptyProject() => new ProjectDtoModel
        {
            Name = Properties.translations.VarStrings.strNewProject,
        };

        private void Save() => JsonFileUtil.SaveValues(DataProvider.ProjectsPath, "json", this.model.Projects.ToList());

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.Save();

            this.Close();
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            this.Save();

            this.needRestart = true;
        }

        private void ButtonNew_Click(object sender, EventArgs e) => this.New();

        private void ButtonRemove_Click(object sender, EventArgs e) => this.Remove();
    }
}
