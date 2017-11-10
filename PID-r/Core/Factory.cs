using System.Collections.Generic;
using System.Linq;
using PID_r.Helpers;

namespace PID_r.Core
{
    internal class Factory
    {
        private readonly List<Detail> details = new List<Detail>();

        public Factory(IEnumerable<Workbench> workbenches, IEnumerable<Detail> details)
        {
            this.details.AddRange(details);
        }

        public void Simulate()
        {
            var plans = details.Select(detail => new DetailWorkplan(detail)).ToList();
            do
            {
                SimulatePlan(plans);
            } while (CombHelper.NextMultyPermutation(plans.Select(plan => plan.PlanList)));
        }

        private static void SimulatePlan(List<DetailWorkplan> plans)
        {
            int totalTime = 0;
            var detailToPlan = new Dictionary<Detail, DetailWorkplan>();
            var factories = new HashSet<Workbench>(plans.SelectMany(plan => plan.PlanList));

            foreach (var plan in plans)
            {
                detailToPlan[plan.Detail] = plan;
                plan.NextWorkbenchIndex = 0;
            }

            while (plans.Any(plan => plan.NextWorkbench != null))
            {
                
            }

        }

        public class DetailWorkplan
        {
            public DetailWorkplan(Detail detail)
            {
                Detail = detail;
                PlanList = new List<Workbench>(detail.OrdersDictionary.Keys);
                PlanList.Sort();
            }

            public Detail Detail { get; }
            public List<Workbench> PlanList { get; }
            public int NextWorkbenchIndex { get; set; }

            public Workbench NextWorkbench => NextWorkbenchIndex < PlanList.Count ? PlanList[NextWorkbenchIndex] : null;
        }
    }
}
