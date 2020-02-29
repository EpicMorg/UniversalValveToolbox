﻿using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class ProjectDtoModel : DtoModel {
        private int engine;
        private string path;
        private string name;
        private string args;

        public ProjectDtoModel() {
            this.engine = 0;
            this.path = "";
            this.name = "";
            this.args = "";
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

        public override string ToString() => name ?? "";
    }
}