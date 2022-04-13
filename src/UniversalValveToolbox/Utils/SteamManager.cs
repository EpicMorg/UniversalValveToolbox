using Steamworks;
using System.Diagnostics;
using UniversalValveToolbox.Model.ViewModel;

namespace UniversalValveToolbox.Utils {
    static class SteamManager {
        public static SteamDataViewModel SteamData {
            get {
                var result = new SteamDataViewModel();

                result.SteamPid = Process.GetProcessesByName("steam")[0]?.Id ?? 0;
                result.UserNameSteam = SteamClient.Name ?? null;

                return result;
            }            
        }
    }
}
