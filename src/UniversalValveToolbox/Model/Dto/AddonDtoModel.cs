using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Utils.Dto {
    public class AddonDtoModel : DtoModel {
        private int[] engine;
        private string args;
        private string bin;
        private string name;
        private string category;

        public int[] Engine {
            get => this.engine;
            set => this.UpdateField(value, ref this.engine);
        }

        public string Name {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }

        public string Category {
            get => this.category;
            set => this.UpdateField(value, ref this.category);
        }

        public string Bin {
            get => this.bin;
            set => this.UpdateField(value, ref this.bin);
        }

        public string Args {
            get => this.args;
            set => this.UpdateField(value, ref this.args);
        }
    }

}
