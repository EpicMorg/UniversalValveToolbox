using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalValveToolbox.Base;
using UniversalValveToolbox.Model.Dto;

namespace UniversalValveToolbox.Model.ViewModel {
    class FormProjectViewModel: DtoModel {
        private ProjectDtoModel[] projects;
        private int selectProjectIndex = 0;

        private EngineDtoModel[] engines;
        private int selectEngineIndex = 0;

        public FormProjectViewModel(ProjectDtoModel[] projects, EngineDtoModel[] engines) {
            this.projects = projects;
            this.engines = engines;
        }

        public ProjectDtoModel[] Projects {
            get => projects;
            set {
                UpdateField(value, ref projects);

                SelectProjectIndex = 0;
            }
        }

        public int SelectProjectIndex {
            get => selectProjectIndex;
            set {
                ForceUpdateField(value, ref selectProjectIndex);
                OnPropertyChanged(nameof(SelectProject));

                var index = Math.Max(engines.ToList().FindIndex(engine => engine.Appid == SelectProject.Engine), 0);
  
                SelectEngineIndex = index;
            }
        }

        public ProjectDtoModel SelectProject => projects[selectProjectIndex];

        public EngineDtoModel[] Engines {
            get => engines;
            set {
                UpdateField(value, ref engines);

                SelectEngineIndex = 0;
            }
        }

        public int SelectEngineIndex {
            get => selectEngineIndex;
            set {
                ForceUpdateField(value, ref selectEngineIndex);
                OnPropertyChanged(nameof(SelectEngine));
            }
        }

        public EngineDtoModel SelectEngine => engines[selectEngineIndex];
    }
}
