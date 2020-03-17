namespace UniversalValveToolbox.Model.Dto
{
    using UniversalValveToolbox.Base;

    public class ProjectDtoModel : DtoModel
    {
        private int engine;
        private string path;
        private string name;
        private string args;

        public ProjectDtoModel()
        {
            this.engine = 0;
            this.path = string.Empty;
            this.name = string.Empty;
            this.args = string.Empty;
        }

        public int Engine
        {
            get => this.engine;
            set => this.UpdateField(value, ref this.engine);
        }

        public string Path
        {
            get => this.path;
            set => this.UpdateField(value, ref this.path);
        }

        public string Name
        {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }

        public string Args
        {
            get => this.args;
            set => this.UpdateField(value, ref this.args);
        }

        public override string ToString() => this.name ?? string.Empty;
    }
}
