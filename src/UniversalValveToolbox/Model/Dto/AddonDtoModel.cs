using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class AddonDtoModel : DtoModel {
        private int[] engines;
        private string args;
        private string bin;
        private string name;
        private string category;

        public AddonDtoModel() {
            this.engines = new int[0];
            this.args = string.Empty;
            this.bin = string.Empty;
            this.name = string.Empty;
            this.category = string.Empty;
        }

        public int[] Engines {
            get => engines;
            set => UpdateField(value, ref engines);
        }

        public string Name {
            get => name;
            set => UpdateField(value, ref name);
        }

        public string Category {
            get => category;
            set => UpdateField(value, ref category);
        }

        public string Bin {
            get => bin;
            set => UpdateField(value, ref bin);
        }

        public string Args {
            get => args;
            set => UpdateField(value, ref args);
        }

        public override string ToString() => name ?? string.Empty;
    }

}
