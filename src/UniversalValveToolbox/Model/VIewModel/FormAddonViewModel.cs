using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalValveToolbox.Base;
using UniversalValveToolbox.Model.Dto;

namespace UniversalValveToolbox.Model.ViewModel {
    class FormAddonViewModel: DtoModel {
        private AddonDtoModel[] addons;
        private int selectAddonIndex = 0;

        private EngineDtoModel[] engines;
        private List<int> arraySelectAddonIndex = new List<int>();

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

                var indexs = SelectAddon.Engines;
                ArraySelectAddonIndex = indexs;
            }
        }

        public AddonDtoModel SelectAddon => addons[selectAddonIndex];

        public EngineDtoModel[] Engines {
            get => engines;
            set {
                UpdateField(value, ref engines);
            }
        }

        public int[] ArraySelectAddonIndex {
            get => arraySelectAddonIndex.ToArray();
            set {
                ForceUpdateField(value.ToList(), ref arraySelectAddonIndex);
                //OnPropertyChanged(nameof(ArraySelectEngine));
            }
        }

        //public EngineDtoModel[] ArraySelectEngine {
        //    get {
        //        List<EngineDtoModel> result = new List<EngineDtoModel>();
        //        foreach (var index in arraySelectAddonIndex) {
        //            result.Add(engines[index]);
        //        }

        //        return result.ToArray();
        //    }
        //}
    }
}
