using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace UniversalValveToolbox.Base {
    public abstract class DtoModel : INotifyPropertyChanged {

        protected bool ForceUpdateField<T>(T value, ref T field, [CallerMemberName]string name = null) {
            field = value;
            OnPropertyChanged(name);

            return true;
        }

        protected bool UpdateField<T>(T value, ref T field, [CallerMemberName]string name = null) {
            var updated = !EqualityComparer<T>.Default.Equals(value, field);
            if (updated) {
                field = value;
                OnPropertyChanged(name);
            }
            return updated;
        }

        protected void OnPropertyChanged([CallerMemberName]string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
