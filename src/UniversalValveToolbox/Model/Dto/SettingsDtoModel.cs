using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class SettingsDtoModel : DtoModel {
        private string lastSelectedProject;
        private string[] availableLanguages;
        private string language;

        public string LastSelectedProject {
            get => lastSelectedProject;
            set => UpdateField(value, ref lastSelectedProject);
        }

        public string[] AvailableLanguages {
            get => availableLanguages;
            set => UpdateField(value, ref availableLanguages);
        }

        public string Language {
            get => language;
            set => UpdateField(value, ref language);
        }
    }
}
