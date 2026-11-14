using UniversalValveToolbox.Model.ViewModel;
using UniversalValveToolbox.Steam;

namespace UniversalValveToolbox.Utils {
    static class SteamManager {
        public static SteamDataViewModel SteamData {
            get {
                var result = new SteamDataViewModel();

                // no steam process -> 0 (GetProcessesByName("steam")[0] used to throw here)
                using (var steam = SteamApi.SteamProcess) {
                    result.SteamPid = steam?.Id ?? 0;
                }
                result.UserNameSteam = SteamApi.UserName;

                return result;
            }
        }
    }
}
