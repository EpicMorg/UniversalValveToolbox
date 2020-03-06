using kasthack.binding.wf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UniversalValveToolbox.Model.Dto;
using UniversalValveToolbox.Model.Provider;
using UniversalValveToolbox.Model.ViewModel;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox {
    public partial class FormProfiles : Form {
        private bool needRestart = false;

        private DataProvider dataProvider = new DataProvider();

        private FormProjectViewModel model;


        public FormProfiles() {
            InitializeComponent();

            model = new FormProjectViewModel(dataProvider.Projects, dataProvider.Engines);

            UpdateComboBoxProject();
            UpdateComboBoxEngine();

            textBox1.Bind(a => a.Text, model, a => a.SelectProject.Name);
            textBoxPath.Bind(a => a.Text, model, a => a.SelectProject.Path);
            textBoxArgs.Bind(a => a.Text, model, a => a.SelectProject.Args);

            comboBox_Mod.Bind(a => a.SelectedIndex, model, a => a.SelectProjectIndex);
            comboBoxEngine.Bind(a => a.SelectedIndex, model, a => a.SelectEngineIndex);
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            UpdateComboBoxEngine();
        }

        private void UpdateComboBoxProject() {
            if (model.Projects.Length == 0) {
                New();
            } else {
                comboBox_Mod.Items.Clear();
                comboBox_Mod.Items.AddRange(model.Projects);

                comboBox_Mod.SelectedIndex = model.SelectProjectIndex;
            }
        }

        private void UpdateComboBoxEngine() {
            comboBoxEngine.Items.Clear();
            comboBoxEngine.Items.AddRange(model.Engines);

            comboBoxEngine.SelectedIndex = model.SelectEngineIndex;
        }

        private void FormEditProfile_Load(object sender, EventArgs e) {

        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            if (needRestart) {
                DialogResult = DialogResult.OK;
            }

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

        private void comboBoxEngine_SelectedIndexChanged(object sender, EventArgs e) {
            if (model.SelectProject != null)
                model.SelectProject.Engine = ((EngineDtoModel)comboBoxEngine.SelectedItem).Appid;
        }

        private void comboBox_Mod_SelectedIndexChanged(object sender, EventArgs e) {
            model.SelectProjectIndex = comboBox_Mod.SelectedIndex;
        }

        private void Remove() {
            var newProjectList = new List<ProjectDtoModel>(model.Projects);
            newProjectList.RemoveAt(model.SelectProjectIndex);

            model.Projects = newProjectList.ToArray();

            UpdateComboBoxProject();
        }

        private void New() {
            var newProject = CreateNewEmptyProject();

            var newProjectList = new List<ProjectDtoModel>(model.Projects);
            newProjectList.Insert(0, newProject);

            model.Projects = newProjectList.ToArray();

            UpdateComboBoxProject();
        }

        private ProjectDtoModel CreateNewEmptyProject() {
            var newProject = new ProjectDtoModel();
            newProject.Name = Properties.translations.VarStrings.strNewProject;

            return newProject;
        }

        private void Save() {
            JsonFileUtil.SaveValues(DataProvider.ProjectsPath, "json", model.Projects.ToList());
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            Save();

            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e) {
            Save();

            needRestart = true;
        }

        private void buttonNew_Click(object sender, EventArgs e) {
            New();
        }

        private void buttonRemove_Click(object sender, EventArgs e) {
            Remove();
        }
    }
}
