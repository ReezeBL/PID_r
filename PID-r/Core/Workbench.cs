using System;

namespace PID_r.Core
{
    internal class Workbench : IComparable<Workbench>
    {
        private Detail detailInWork;

        public Workbench(string type)
        {
            Type = type;
            CachedType = type.GetHashCode();
        }

        public string Type { get; }
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
    }
}