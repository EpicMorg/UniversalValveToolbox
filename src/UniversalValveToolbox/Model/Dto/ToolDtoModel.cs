namespace UniversalValveToolbox.Model.Dto
{
    using UniversalValveToolbox.Base;

    public class ToolDtoModel : DtoModel
    {
        private string args;
        private string bin;
        private string name;

        public string Args
        {
            get => this.args;
            set => this.UpdateField(value, ref this.args);
        }

        public string Bin
        {
            get => this.bin;
            set => this.UpdateField(value, ref this.bin);
        }

        public string Name
        {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }
    }
}
