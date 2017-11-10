using System.Collections.Generic;

namespace PID_r.Core
{
    public class Detail
    {
        private readonly Dictionary<Workbench, int> ordersDictionary = new Dictionary<Workbench, int>();

        public Detail(string type)
        {
            Type = type;
        }

        public string Type { get; }

        public IReadOnlyDictionary<Workbench, int> OrdersDictionary => ordersDictionary;

        public int GetOrderTime(Workbench workbench)
        {
            return OrdersDictionary.TryGetValue(workbench, out var time) ? time : 0;
        }

        public void SetOrderTime(Workbench workbench, int time)
        {
            ordersDictionary[workbench] = time;
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
