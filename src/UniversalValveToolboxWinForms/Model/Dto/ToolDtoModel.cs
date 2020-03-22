using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class ToolDtoModel : DtoModel {
        private string args;
        private string bin;
        private string name;

        public string Args {
            get => args;
            set => UpdateField(value, ref args);
        }

        public string Bin {
            get => bin;
            set => UpdateField(value, ref bin);
        }

        public string Name {
            get => name;
            set => UpdateField(value, ref name);
        }
    }
}
