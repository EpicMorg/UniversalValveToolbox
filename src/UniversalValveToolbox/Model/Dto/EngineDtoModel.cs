using System.IO;
using System.Linq;
using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.Dto {
    public class EngineDtoModel : DtoModel {
        private int appid;
        private string name;
        private string bin;
        private string[] altBins;
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

        /// <summary>
        /// Other game executables of the same app id, for older builds (e.g. hl2.exe before the game got its own
        /// tf_win64.exe). Optional; the first of Bin + AltBins that exists in the install is used.
        /// </summary>
        public string[] AltBins {
            get => altBins;
            set => UpdateField(value, ref altBins);
        }

        public ToolDtoModel[] Tools {
            get => tools;
            set => UpdateField(value, ref tools);
        }

        /// <summary>Bin, or the first AltBins entry that exists under <paramref name="enginePath"/>; Bin if none exists.</summary>
        public string ResolveBin(string enginePath) {
            if (enginePath == null || altBins == null || altBins.Length == 0)
                return bin;
            foreach (var candidate in new[] { bin }.Concat(altBins)) {
                if (!string.IsNullOrEmpty(candidate) && File.Exists(Path.Combine(enginePath, candidate)))
                    return candidate;
            }
            return bin;
        }

        public override string ToString() => name;
    }
}
