using EpicMorg.SteamPathsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalValveToolbox.Utils {
    class EnvUtils {
        public static void PrepareVProject(string data) {
            Environment.SetEnvironmentVariable("VProject", data, EnvironmentVariableTarget.User);
        }

        public static void PrepareVMod(string data) {
            Environment.SetEnvironmentVariable("VMod", data, EnvironmentVariableTarget.User);
        }

        public static void PrepareVGame(string data) {
            Environment.SetEnvironmentVariable("VGame", data, EnvironmentVariableTarget.User);
        }

        public static void PrepareVContent(string data) {
            Environment.SetEnvironmentVariable("VContent", data, EnvironmentVariableTarget.User);
        }

        public static void PrepareVTools(string data) {
            Environment.SetEnvironmentVariable("VTools", data, EnvironmentVariableTarget.User);
        }

        public static void PrepareSFMData(string pathProject) {
            var SFMpath = SteamPathsUtil.GetSteamAppManifestDataById(1840)?.Path;

            if (SFMpath == null)
                return;

            PrepareVMod(Path.GetDirectoryName(pathProject));
            PrepareVProject(pathProject);
            PrepareVContent(Path.Combine(SFMpath, "content"));
            PrepareVGame(Path.Combine(SFMpath, "game"));
            PrepareVTools(Path.Combine(SFMpath, "game", "sdktools"));
        }
    }
}
