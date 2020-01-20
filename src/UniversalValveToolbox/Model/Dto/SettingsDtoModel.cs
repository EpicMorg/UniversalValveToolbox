using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class SettingsDtoModel : DtoModel {
        private string defaultProject;
        private int[] availableEnginies;
        private string[] availableLanguages;
        private string language;
        private string theme;

        public string DefaultProject {
            get => defaultProject;
            set => UpdateField(value, ref defaultProject);
        }

        public int[] AvailableEnginies {
            get => availableEnginies;
            set => UpdateField(value, ref availableEnginies);
        }

        public string[] AvailableLanguages {
            get => availableLanguages;
            set => UpdateField(value, ref availableLanguages);
        }

        public string Language {
            get => language;
            set => UpdateField(value, ref language);
        }

        public string Theme {
            get => theme;
            set => UpdateField(value, ref theme);
        }
    }
}
