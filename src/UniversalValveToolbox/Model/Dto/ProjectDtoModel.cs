using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class ProjectDtoModel : DtoModel {
        private int engine;
        private string path;
        private string name;
        private string args;

        public ProjectDtoModel() {
            this.engine = 0;
            this.path = string.Empty;
            this.name = string.Empty;
            this.args = string.Empty;
        }

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
 
        public override string ToString() => name ?? string.Empty;
    }
}
