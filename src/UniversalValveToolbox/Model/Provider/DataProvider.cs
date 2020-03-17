namespace UniversalValveToolbox.Model.Provider
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using UniversalValveToolbox.Model.Dto;
    using UniversalValveToolbox.Utils;

    internal class DataProvider
    {
        public static readonly string JsonRootPath = Path.Combine(Application.StartupPath, "json");
        public static readonly string SettingsPath = Path.Combine(JsonRootPath, "settings.json");
        public static readonly string EnginesPath = Path.Combine(JsonRootPath, "engines");
        public static readonly string ProjectsPath = Path.Combine(JsonRootPath, "projects");
        public static readonly string AddonsPath = Path.Combine(JsonRootPath, "addons");

        public static readonly SettingsDtoModel DefaultSettings = GenerateDefaultSettings();

        public SettingsDtoModel Settings
        {
            get
            {
                this.ValidateJsonFolder();

                return JsonFileUtil.ReadValue<SettingsDtoModel>(SettingsPath, DefaultSettings);
            }

            set
            {
                this.ValidateJsonFolder();

                JsonFileUtil.WriteValue(SettingsPath, value);
            }
        }

        public EngineDtoModel[] Engines
        {
            get
            {
                this.ValidateJsonFolder();
                return JsonFileUtil.ReadValues<EngineDtoModel>(EnginesPath);
            }
        }

        public AddonDtoModel[] Addons
        {
            get
            {
                this.ValidateJsonFolder();
                return JsonFileUtil.ReadValues<AddonDtoModel>(AddonsPath);
            }
        }

        public ProjectDtoModel[] Projects
        {
            get
            {
                this.ValidateJsonFolder();
                return JsonFileUtil.ReadValues<ProjectDtoModel>(ProjectsPath);
            }
        }

        private void ValidateJsonFolder()
        {
            if (!Directory.Exists(JsonRootPath))
            {
                Directory.CreateDirectory(JsonRootPath);
            }
        }

        private static SettingsDtoModel GenerateDefaultSettings()
        {
            var result = new SettingsDtoModel
            {
                Language = "en-US", // may be fix in future
            };

            var listLang = new List<string>();
            //var resourceSet = Properties.translations.LangDict.ResourceManager.GetResourceSet(new CultureInfo("en-US"), true, true);
            //foreach (DictionaryEntry entry in resourceSet)
            //{
            //    listLang.Add(entry.Key.ToString());
            //}

            result.AvailableLanguages = listLang.ToArray();

            return result;
        }
    }
}
