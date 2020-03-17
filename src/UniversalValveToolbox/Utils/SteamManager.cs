namespace UniversalValveToolbox.Utils
{
    using EpicMorg.SteamPathsLib;
    using UniversalValveToolbox.Model.ViewModel;

    internal static class SteamManager
    {
        public static SteamDataViewModel SteamData
        {
            get
            {
                var result = new SteamDataViewModel
                {
                    SteamPid = SteamPathsUtil.GetActiveProcessSteamData()?.PID ?? 0,
                    UserNameSteam = SteamPathsUtil.GetSteamData()?.LastGameNameUsed ?? null,
                };

                return result;
            }
        }
    }
}
