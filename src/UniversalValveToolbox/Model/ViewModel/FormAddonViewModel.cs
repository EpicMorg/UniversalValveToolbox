using System;
using System.Collections;
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
        private int selectCategoryIndex = 0;

        private EngineDtoModel[] engines;
        private DictionaryEntry[] categories;

        public FormAddonViewModel(AddonDtoModel[] addons, EngineDtoModel[] engines, DictionaryEntry[] categories) {
            this.addons = addons;
            this.engines = engines;
            this.categories = categories;
        }

        public AddonDtoModel[] Addons {
            get => addons;
            set {
                UpdateField(value, ref addons);

                SelectAddonIndex = 0;
            }
        }

        public DictionaryEntry[] Categories {
            get => categories;
            set {
                UpdateField(value, ref categories);

                SelectCategoryIndex = 0;
            }
        }

        public int SelectAddonIndex {
            get => selectAddonIndex;
            set {
                ForceUpdateField(value, ref selectAddonIndex);
                OnPropertyChanged(nameof(SelectAddon));
            }
        }

        public int SelectCategoryIndex {
            get => selectCategoryIndex;
            set {
                ForceUpdateField(value, ref selectCategoryIndex);
                // the chosen category belongs to the selected addon (it was never stored before)
                if (value >= 0 && categories != null && value < categories.Length && SelectAddon != null)
                    SelectAddon.Category = categories[value].Key?.ToString() ?? string.Empty;
                OnPropertyChanged(nameof(SelectCategory));
            }
        }

        public AddonDtoModel SelectAddon {
            get {
                if (addons == null || addons.Length == 0)
                    return null;
                
                return addons[selectAddonIndex]; 
            }
        }

        public DictionaryEntry SelectCategory {
            get { return selectCategoryIndex >= 0 && selectCategoryIndex < categories.Length ? categories[selectCategoryIndex] : default; }
        }

        public EngineDtoModel[] Engines {
            get => engines;
            set {
                UpdateField(value, ref engines);
            }
        }
    }
}
