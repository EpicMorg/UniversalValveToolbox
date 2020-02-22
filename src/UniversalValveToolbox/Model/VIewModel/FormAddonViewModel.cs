using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalValveToolbox.Base;
using UniversalValveToolbox.Model.Dto;
using static System.Windows.Forms.ListView;

namespace UniversalValveToolbox.Model.ViewModel {
    class FormAddonViewModel: DtoModel {
        private AddonDtoModel[] addons;
        private int selectAddonIndex = 0;

        private EngineDtoModel[] engines;

        public FormAddonViewModel(AddonDtoModel[] addons, EngineDtoModel[] engines) {
            this.addons = addons;
            this.engines = engines;
        }

        public AddonDtoModel[] Addons {
            get => addons;
            set {
                UpdateField(value, ref addons);

                SelectAddonIndex = 0;
            }
        }

        public int SelectAddonIndex {
            get => selectAddonIndex;
            set {
                ForceUpdateField(value, ref selectAddonIndex);
                OnPropertyChanged(nameof(SelectAddon));
            }
        }

        public AddonDtoModel SelectAddon => addons[selectAddonIndex];

        public EngineDtoModel[] Engines {
            get => engines;
            set {
                UpdateField(value, ref engines);
            }
        }
    }
}
