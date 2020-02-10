using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalValveToolbox.Base;
using UniversalValveToolbox.Model.Dto;

namespace UniversalValveToolbox.Model.ViewModel {
    class FormAddonViewModel: DtoModel {
        private AddonDtoModel selectAddon;

        public AddonDtoModel SelectAddon {
            get => selectAddon;
            set => UpdateField(value, ref selectAddon);
        }

    }
}
