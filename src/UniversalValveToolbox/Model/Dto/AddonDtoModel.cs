namespace UniversalValveToolbox.Model.Dto
{
    using UniversalValveToolbox.Base;

    public class AddonDtoModel : DtoModel
    {
        private int[] engines;
        private string args;
        private string bin;
        private string name;
        private string category;

        public AddonDtoModel()
        {
            this.engines = new int[0];
            this.args = string.Empty;
            this.bin = string.Empty;
            this.name = string.Empty;
            this.category = string.Empty;
        }

        public int[] Engines
        {
            get => this.engines;
            set => this.UpdateField(value, ref this.engines);
        }

        public string Name
        {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }

        public string Category
        {
            get => this.category;
            set => this.UpdateField(value, ref this.category);
        }

        public string Bin
        {
            get => this.bin;
            set => this.UpdateField(value, ref this.bin);
        }

        public string Args
        {
            get => this.args;
            set => this.UpdateField(value, ref this.args);
        }

        public override string ToString() => this.name ?? string.Empty;
    }
}
