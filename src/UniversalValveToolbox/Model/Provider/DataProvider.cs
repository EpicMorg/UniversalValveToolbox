using System;
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

        private readonly string SettingsPath = Path.Combine(Application.StartupPath, "json", "settings.json");

        public SettingsDtoModel Settings {
            get => JsonFileUtil.ReadValue<SettingsDtoModel>(SettingsPath);
            set => JsonFileUtil.WriteValue(SettingsPath, value);
        }
    }
}
