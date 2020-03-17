namespace UniversalValveToolbox.Model.ViewModel
{
    using UniversalValveToolbox.Base;

    internal class SteamDataViewModel : DtoModel
    {
        private int steamPid;
        private string userNameSteam;

        public int SteamPid
        {
            get => this.steamPid;
            set => this.UpdateField(value, ref this.steamPid);
        }

        public string UserNameSteam
        {
            get => this.userNameSteam;
            set => this.UpdateField(value, ref this.userNameSteam);
        }
    }
}
