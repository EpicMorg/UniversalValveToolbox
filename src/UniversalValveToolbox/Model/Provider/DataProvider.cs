﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using UniversalValveToolbox.Model.Dto;
using UniversalValveToolbox.Utils;

namespace UniversalValveToolbox.Model.Provider {
    class DataProvider {

        public static readonly string SettingsPath = Path.Combine(Application.StartupPath, "json", "settings.json");
        public static readonly string EnginesPath = Path.Combine(Application.StartupPath, "json", "engines");
        public static readonly string ProjectsPath = Path.Combine(Application.StartupPath, "json", "projects");
        public static readonly string AddonsPath = Path.Combine(Application.StartupPath, "json", "addons");


        public SettingsDtoModel Settings {
            get => JsonFileUtil.ReadValue<SettingsDtoModel>(SettingsPath);
            set => JsonFileUtil.WriteValue(SettingsPath, value);
        }

        public EngineDtoModel[] Engines {
            get => JsonFileUtil.ReadValues<EngineDtoModel>(EnginesPath);
        }

        public AddonDtoModel[] Addons {
            get => JsonFileUtil.ReadValues<AddonDtoModel>(AddonsPath);
        }

        public ProjectDtoModel[] Projects {
            get => JsonFileUtil.ReadValues<ProjectDtoModel>(ProjectsPath);
        }
    }
}