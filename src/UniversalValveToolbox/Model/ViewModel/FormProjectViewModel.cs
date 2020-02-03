using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalValveToolbox.Base;
using UniversalValveToolbox.Model.Dto;

namespace UniversalValveToolbox.Model.ViewModel {
    class FormProjectViewModel: DtoModel {
        private ProjectDtoModel selectProject;

        public ProjectDtoModel SelectProject {
            get => selectProject;
            set => UpdateField(value, ref selectProject);
        }
    }
}
