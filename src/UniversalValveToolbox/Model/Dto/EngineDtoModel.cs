namespace UniversalValveToolbox.Model.Dto
{
    using UniversalValveToolbox.Base;

    public class EngineDtoModel : DtoModel
    {
        private int appid;
        private string name;
        private string bin;
        private ToolDtoModel[] tools;

        public int Appid
        {
            get => this.appid;
            set => this.UpdateField(value, ref this.appid);
        }

        public string Name
        {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }

        public string Bin
        {
            get => this.bin;
            set => this.UpdateField(value, ref this.bin);
        }

        public ToolDtoModel[] Tools
        {
            get => this.tools;
            set => this.UpdateField(value, ref this.tools);
        }

        public override string ToString() => this.name;
    }
}
