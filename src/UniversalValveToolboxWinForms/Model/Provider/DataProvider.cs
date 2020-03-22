using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using UniversalValveToolbox.Model.Dto;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox.Model.Provider {
    class DataProvider {

        public static readonly string JsonRootPath = Path.Combine(Application.StartupPath, "json");
        public static readonly string SettingsPath = Path.Combine(JsonRootPath, "settings.json");
        public static readonly string EnginesPath = Path.Combine(JsonRootPath, "engines");
        public static readonly string ProjectsPath = Path.Combine(JsonRootPath, "projects");
        public static readonly string AddonsPath = Path.Combine(JsonRootPath, "addons");

        public static readonly SettingsDtoModel DefaultSettings = GenerateDefaultSettings();



        public SettingsDtoModel Settings {
            get {
                validateJsonFolder();

                return JsonFileUtil.ReadValue<SettingsDtoModel>(SettingsPath, DefaultSettings);
            }

            set {
                validateJsonFolder();

                JsonFileUtil.WriteValue(SettingsPath, value);
            }
        }

        public EngineDtoModel[] Engines {
            get {
                validateJsonFolder();
                return JsonFileUtil.ReadValues<EngineDtoModel>(EnginesPath);
            }
        }

        public AddonDtoModel[] Addons {
            get {
                validateJsonFolder();
                return JsonFileUtil.ReadValues<AddonDtoModel>(AddonsPath);
            }
        }

        public ProjectDtoModel[] Projects {
            get {
                validateJsonFolder();
                return JsonFileUtil.ReadValues<ProjectDtoModel>(ProjectsPath);
            }
        }

        private void validateJsonFolder() {
            if (!Directory.Exists(JsonRootPath)) {
                Directory.CreateDirectory(JsonRootPath);
            } 
        }

        private static SettingsDtoModel GenerateDefaultSettings()  {
            var result = new SettingsDtoModel();
            result.Language = "en-US"; //may be fix in future

            var listLang = new List<string>();
            var resourceSet = Properties.translations.LangDict.ResourceManager.GetResourceSet(new CultureInfo("en-US"), true, true);
            foreach (DictionaryEntry entry in resourceSet) {
                listLang.Add(entry.Key.ToString());
            }

            result.AvailableLanguages = listLang.ToArray();

            return result;

        }
    }
}
