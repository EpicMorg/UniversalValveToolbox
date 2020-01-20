using EpicMorg.SteamPathsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalValveToolbox.Model.ViewModel;

namespace UniversalValveToolbox.Utils {
    static class SteamManager {
        public static SteamDataViewModel SteamData {
            get {
                var result = new SteamDataViewModel();
                result.SteamPid = SteamPathsUtil.GetActiveProcessSteamData().PID;
                result.UserNameSteam = SteamPathsUtil.GetSteamData().LastGameNameUsed;

                return result;
            }            
        }
    }
}
