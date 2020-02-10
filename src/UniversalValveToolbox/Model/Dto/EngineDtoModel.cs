using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class EngineDtoModel : DtoModel {
        private int appid;
        private string name;
        private string bin;
        private ToolDtoModel[] tools;

        public int Appid {
            get => appid;
            set => UpdateField(value, ref appid);
        }

        public string Name {
            get => name;
            set => UpdateField(value, ref name);
        }

        public string Bin {
            get => bin;
            set => UpdateField(value, ref bin);
        }

        public ToolDtoModel[] Tools {
            get => tools;
            set => UpdateField(value, ref tools);
        }

        public override string ToString() => name;
    }
}
