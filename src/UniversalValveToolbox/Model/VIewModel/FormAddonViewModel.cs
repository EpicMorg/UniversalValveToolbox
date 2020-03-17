namespace UniversalValveToolbox.Model.ViewModel
{
    using System.Collections;
    using UniversalValveToolbox.Base;
    using UniversalValveToolbox.Model.Dto;

    internal class FormAddonViewModel : DtoModel
    {
        private AddonDtoModel[] addons;
        private int selectAddonIndex = 0;
        private int selectCategoryIndex = 0;

        private EngineDtoModel[] engines;
        private DictionaryEntry[] categories;

        public FormAddonViewModel(AddonDtoModel[] addons, EngineDtoModel[] engines, DictionaryEntry[] categories)
        {
            this.addons = addons;
            this.engines = engines;
            this.categories = categories;
        }

        public AddonDtoModel[] Addons
        {
            get => this.addons;
            set
            {
                this.UpdateField(value, ref this.addons);

                this.SelectAddonIndex = 0;
            }
        }

        public DictionaryEntry[] Categories
        {
            get => this.categories;
            set
            {
                this.UpdateField(value, ref this.categories);

                this.SelectCategoryIndex = 0;
            }
        }

        public int SelectAddonIndex
        {
            get => this.selectAddonIndex;
            set
            {
                this.ForceUpdateField(value, ref this.selectAddonIndex);
                this.OnPropertyChanged(nameof(this.SelectAddon));
            }
        }

        public int SelectCategoryIndex
        {
            get => this.selectCategoryIndex;
            set
            {
                this.ForceUpdateField(value, ref this.selectCategoryIndex);
                this.OnPropertyChanged(nameof(this.SelectCategory));
            }
        }

        public AddonDtoModel SelectAddon => this.addons == null || this.addons.Length == 0 ? null : this.addons[this.selectAddonIndex];

        public DictionaryEntry SelectCategory => this.categories[this.selectCategoryIndex];

        public EngineDtoModel[] Engines
        {
            get => this.engines;
            set => this.UpdateField(value, ref this.engines);
        }
    }
}
