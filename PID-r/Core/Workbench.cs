using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PID_r.Annotations;

namespace PID_r.Core
{
    public class Workbench : IComparable<Workbench>, INotifyPropertyChanged
    {
        private Detail detailInWork;
        private string type;

        public Workbench(string type)
        {
            Type = type;
            CachedType = type.GetHashCode();
        }

        public string Type
        {
            get => type;
            set
            {
                if (value == type) return;
                type = value;
                OnPropertyChanged();
            }
        }

        private int CachedType { get; }
        public bool IsBusy => OrderTime > 0;
        public int OrderTime { get; private set; }

        public void CreateOrder(Detail detail)
        {
            if(IsBusy)
                throw new AccessViolationException("Current workbench is allready busy!");

            OrderTime += detail.GetOrderTime(this);
            detailInWork = detail;
        }

        public Detail Tick(int time)
        {
            if (!IsBusy)
                return null;
            OrderTime -= time;
            return OrderTime <= 0 ? detailInWork : null;
        }

        public override string ToString()
        {
            return Type;
        }

        public int CompareTo(Workbench other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return CachedType.CompareTo(other.CachedType);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}