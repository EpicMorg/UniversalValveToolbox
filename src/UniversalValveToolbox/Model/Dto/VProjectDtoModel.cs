using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class VProjectDtoModel : DtoModel {
        private int engine;
        private string path;
        private string name;
        private string args;

        public int Engine {
            get => engine;
            set => UpdateField(value, ref engine);
        }

        public string Path {
            get => path;
            set => UpdateField(value, ref path);
        }

        public string Name {
            get => name;
            set => UpdateField(value, ref name);
        }

        public string Args {
            get => args;
            set => UpdateField(value, ref args);
        }
    }
}
