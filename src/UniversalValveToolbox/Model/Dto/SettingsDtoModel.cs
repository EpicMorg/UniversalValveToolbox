using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class SettingsDtoModel : DtoModel {
        private string lastSelectedProject;
        private string[] availableLanguages;
        private string language;
        private string devenginepath = "C://";
        private string devenginename = "My Future Game (engine)";
        private ToolboxMod toolboxmod = ToolboxMod.retail;
        private uint toolsappid = 480;
        private uint bundleappsid = 480;


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

        [JsonConverter(typeof(StringEnumConverter))]
        public ToolboxMod ToolboxMod
        {
            get => toolboxmod;
            set => UpdateField(value, ref toolboxmod);
        }
        public uint ToolsAppId
        {
            get => toolsappid;
            set => UpdateField(value, ref toolsappid);
        }
        public uint BundleAppID
        {
            get => bundleappsid;
            set => UpdateField(value, ref bundleappsid);
        }
        public string DevEnginePath
        {
            get => devenginepath;
            set => UpdateField(value, ref devenginepath);
        }
        public string DevEngineName
        {
            get => devenginename;
            set => UpdateField(value, ref devenginename);
        }
    }
    public enum ToolboxMod {
        retail, bundle, dev
    }
}
