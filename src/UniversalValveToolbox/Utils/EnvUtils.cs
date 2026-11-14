using System;
using System.Collections.Generic;
using System.IO;
using UniversalValveToolbox.Model.Dto;

namespace UniversalValveToolbox.Utils {
    /// <summary>
    /// Source SDK environment for launched tools: VProject (game/mod folder of the project) and, for Source Filmmaker,
    /// VMod / VContent / VGame / VTools.
    /// </summary>
    internal static class EnvUtils {
        public const int SfmAppId = 1840;

        /// <summary>Variables for a launch: none without a project; the SFM set only for SFM.</summary>
        public static Dictionary<string, string> ForLaunch(ProjectDtoModel project, EngineDtoModel engine, string enginePath) {
            var env = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(project?.Path))
                return env;

            env["VProject"] = project.Path;

            if (engine?.Appid == SfmAppId && !string.IsNullOrEmpty(enginePath)) {
                env["VMod"] = new DirectoryInfo(project.Path).Name;
                env["VContent"] = Path.Combine(enginePath, "content");
                env["VGame"] = Path.Combine(enginePath, "game");
                env["VTools"] = Path.Combine(enginePath, "game", "sdktools");
            }
            return env;
        }

        /// <summary>
        /// Also store the variables in the user environment (as the Source SDK's vconfig does), so tools started
        /// outside the launcher see the last project. Writes only values that changed: every user-level write
        /// broadcasts WM_SETTINGCHANGE to all windows, which made each click slow.
        /// </summary>
        public static void ExportToUser(Dictionary<string, string> env) {
            foreach (var pair in env) {
                if (Environment.GetEnvironmentVariable(pair.Key, EnvironmentVariableTarget.User) != pair.Value)
                    Environment.SetEnvironmentVariable(pair.Key, pair.Value, EnvironmentVariableTarget.User);
            }
        }
    }
}
