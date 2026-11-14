using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    /// <summary>
    /// A game build in a local folder (not installed through Steam), listed next to the Steam games as "Name [local]".
    /// Its exe and tools come from the catalog entry <see cref="Template"/> (json\engines\&lt;Template&gt;.json).
    /// </summary>
    public class LocalEngineDtoModel : DtoModel {
        private string name;
        private string path;
        private int template;

        public string Name {
            get => name;
            set => UpdateField(value, ref name);
        }

        /// <summary>Game root folder; environment variables are expanded.</summary>
        public string Path {
            get => path;
            set => UpdateField(value, ref path);
        }

        /// <summary>App id of the catalog entry the tools are taken from.</summary>
        public int Template {
            get => template;
            set => UpdateField(value, ref template);
        }
    }
}
