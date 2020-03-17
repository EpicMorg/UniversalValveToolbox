namespace UniversalValveToolbox.Base
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class DtoModel : INotifyPropertyChanged
    {
        protected bool ForceUpdateField<T>(T value, ref T field, [CallerMemberName]string name = null)
        {
            field = value;
            this.OnPropertyChanged(name);

            return true;
        }

        protected bool UpdateField<T>(T value, ref T field, [CallerMemberName]string name = null)
        {
            var updated = !EqualityComparer<T>.Default.Equals(value, field);
            if (updated)
            {
                field = value;
                this.OnPropertyChanged(name);
            }

            return updated;
        }

        protected void OnPropertyChanged([CallerMemberName]string name = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
