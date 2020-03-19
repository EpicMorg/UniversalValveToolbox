using EpicMorg.SteamPathsLib;
using UniversalValveToolbox.Model.ViewModel;

namespace UniversalValveToolbox.Utils {
    static class SteamManager {
        public static SteamDataViewModel SteamData {
            get {
                var result = new SteamDataViewModel();
                result.SteamPid = SteamPathsUtil.GetActiveProcessSteamData()?.PID ?? 0;
                result.UserNameSteam = SteamPathsUtil.GetSteamData()?.LastGameNameUsed ?? null;

                return result;
            }            
        }
    }
}
