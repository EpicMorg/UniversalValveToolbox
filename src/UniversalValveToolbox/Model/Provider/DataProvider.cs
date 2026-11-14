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

        // settings.json exists but is broken: run with defaults, never write over the user's file
        private static bool settingsBroken;
        private static bool settingsErrorShown;

        public SettingsDtoModel Settings {
            get {
                validateJsonFolder();

                // a fresh default object each time: callers modify what they get
                var result = JsonFileUtil.ReadValue(SettingsPath, GenerateDefaultSettings(), out var error);
                settingsBroken = error != null;
                if (settingsBroken && !settingsErrorShown) {
                    settingsErrorShown = true;
                    MessageBox.Show($"{SettingsPath} cannot be read, default settings are used until it is fixed." +
                        $"{Environment.NewLine}The launcher does not change the file.{Environment.NewLine}{Environment.NewLine}{error}",
                        "Universal Valve Toolbox", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return result;
            }

            set {
                validateJsonFolder();

                if (settingsBroken)
                    return;
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
