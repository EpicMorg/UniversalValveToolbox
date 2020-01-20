using System;
using UniversalValveToolbox.Base;
using UniversalValveToolbox.Model.Dto;
using UniversalValveToolbox.Model.Provider;

namespace UniversalValveToolbox.Model.VIewModel {
    public class SettingsViewModel : DtoModel {
        private readonly SettingsDtoModel settings;
        private readonly LanguageProvider languageProvider;

        private int selectedLanguage;

        public int SelectedLanguageIndex {
            get => selectedLanguage;
            set {
                if (UpdateField(value, ref selectedLanguage)) {
                    settings.Language = settings.AvailableLanguages[selectedLanguage];
                }
            }
        }

        public string[] Languages => languageProvider.Languages;
        private string SelectedLanguage => Languages[SelectedLanguageIndex];

        public SettingsViewModel(SettingsDtoModel settings, LanguageProvider languageProvider) {
            this.settings = settings;
            this.languageProvider = languageProvider;

            SelectedLanguageIndex = Math.Max(0, Array.IndexOf(settings.AvailableLanguages, settings.Language));
        }
    }
}
