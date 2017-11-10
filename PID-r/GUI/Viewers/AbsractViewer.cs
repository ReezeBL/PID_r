using System.ComponentModel;
using System.Runtime.CompilerServices;
using PID_r.Annotations;

namespace PID_r.GUI.Viewers
{
    public abstract class AbsractViewer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}