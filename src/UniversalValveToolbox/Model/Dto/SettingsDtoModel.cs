namespace UniversalValveToolbox.Model.Dto
{
    using UniversalValveToolbox.Base;

    public class SettingsDtoModel : DtoModel
    {
        private string lastSelectedProject;
        private string[] availableLanguages;
        private string language;

        public string LastSelectedProject
        {
            get => this.lastSelectedProject;
            set => this.UpdateField(value, ref this.lastSelectedProject);
        }

        public string[] AvailableLanguages
        {
            get => this.availableLanguages;
            set => this.UpdateField(value, ref this.availableLanguages);
        }

        public string Language
        {
            get => this.language;
            set => this.UpdateField(value, ref this.language);
        }
    }
}
