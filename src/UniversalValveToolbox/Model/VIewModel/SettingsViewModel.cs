namespace UniversalValveToolbox.Model.ViewModel
{
    using System;
    using UniversalValveToolbox.Base;
    using UniversalValveToolbox.Model.Dto;
    using UniversalValveToolbox.Model.Provider;

    public class SettingsViewModel : DtoModel
    {
        private readonly SettingsDtoModel settings;
        private readonly LanguageProvider languageProvider;

        private int selectedLanguage;

        public int SelectedLanguageIndex
        {
            get => this.selectedLanguage;
            set
            {
                if (this.UpdateField(value, ref this.selectedLanguage))
                {
                    this.settings.Language = this.settings.AvailableLanguages[this.selectedLanguage];
                }
            }
        }

        public string[] Languages => this.languageProvider.Languages;

        private string SelectedLanguage => this.Languages[this.SelectedLanguageIndex];

        public SettingsViewModel(SettingsDtoModel settings, LanguageProvider languageProvider)
        {
            this.settings = settings;
            this.languageProvider = languageProvider;

            this.SelectedLanguageIndex = Math.Max(0, Array.IndexOf(settings.AvailableLanguages, settings.Language));
        }
    }
}
