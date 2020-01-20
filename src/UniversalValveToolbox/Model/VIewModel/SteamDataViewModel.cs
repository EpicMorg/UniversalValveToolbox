using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalValveToolbox.Base;

namespace UniversalValveToolbox.Model.ViewModel {
    class SteamDataViewModel : DtoModel {
        private int steamPid;
        private string userNameSteam;

        public int SteamPid {
            get => steamPid;
            set => UpdateField(value, ref steamPid);
        }

        public string UserNameSteam {
            get => userNameSteam;
            set => UpdateField(value, ref userNameSteam);
        }
    }
}
