namespace UniversalValveToolbox.Model.ViewModel
{
    using System;
    using System.Linq;
    using UniversalValveToolbox.Base;
    using UniversalValveToolbox.Model.Dto;

    internal class FormProjectViewModel : DtoModel
    {
        private ProjectDtoModel[] projects;
        private int selectProjectIndex = 0;

        private EngineDtoModel[] engines;
        private int selectEngineIndex = 0;

        public FormProjectViewModel(ProjectDtoModel[] projects, EngineDtoModel[] engines)
        {
            this.projects = projects;
            this.engines = engines;
        }

        public ProjectDtoModel[] Projects
        {
            get => this.projects;
            set
            {
                this.UpdateField(value, ref this.projects);

                this.SelectProjectIndex = 0;
            }
        }

        public int SelectProjectIndex
        {
            get => this.selectProjectIndex;
            set
            {
                this.ForceUpdateField(value, ref this.selectProjectIndex);
                this.OnPropertyChanged(nameof(this.SelectProject));

                var index = Math.Max(this.engines.ToList().FindIndex(engine => engine.Appid == this.SelectProject?.Engine), -1);

                this.SelectEngineIndex = index;
            }
        }

        public ProjectDtoModel SelectProject => this.projects == null || this.projects.Length == 0 ? null : this.projects[this.selectProjectIndex];

        public EngineDtoModel[] Engines
        {
            get => this.engines;
            set
            {
                this.UpdateField(value, ref this.engines);

                this.SelectEngineIndex = 0;
            }
        }

        public int SelectEngineIndex
        {
            get => this.selectEngineIndex;
            set
            {
                this.ForceUpdateField(value, ref this.selectEngineIndex);
                this.OnPropertyChanged(nameof(this.SelectEngine));
            }
        }

        public EngineDtoModel SelectEngine => this.engines[this.selectEngineIndex];
    }
}
