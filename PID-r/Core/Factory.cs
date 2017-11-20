using System.Collections.Generic;
using System.Linq;
using PID_r.Helpers;

namespace PID_r.Core
{
    public static class Factory
    {

        public static Dictionary<Workbench, List<WorkbenchTimestamp>> Simulate(IEnumerable<Detail> details, out int minTime, out int maxTime)
        {
            var plans = details.Select(detail => new DetailWorkplan(detail)).ToList();
            var maxWorkbecnhCount = plans.Max(plan => plan.Detail.OrdersDictionary.Count);
            var priorities = Enumerable.Range(1, maxWorkbecnhCount)
                .Select(_ => Enumerable.Range(1, plans.Count).ToList()).ToList();


            minTime = int.MaxValue;
            maxTime = int.MinValue;

            Dictionary<Workbench, List<WorkbenchTimestamp>> timeStamps = null;

            do
            {
                var planTime = SimulatePlan(plans, priorities, out var t);
                if (planTime < minTime)
                {
                    minTime = planTime;
                    timeStamps = t;
                }

                if (maxTime < planTime)
                    maxTime = planTime;

            } while (CombHelper.NextMultyPermutation(priorities));

            return timeStamps;
        }

        private static int SimulatePlan(IReadOnlyCollection<DetailWorkplan> plans, IReadOnlyList<List<int>> priorities, out Dictionary<Workbench, List<WorkbenchTimestamp>> timeStamps)
        {
            var totalTime = 0;
            var detailToPlan = new Dictionary<Detail, DetailWorkplan>();
            var factories = new HashSet<Workbench>(plans.SelectMany(plan => plan.PlanList));

            var index = 0;
            var plansWithIndex = plans.Select(plan => new {Plan = plan, Index = index++}).ToList();
            timeStamps = factories.ToDictionary(workbench => workbench, workbench => new List<WorkbenchTimestamp>());

            foreach (var plan in plans)
            {
                detailToPlan[plan.Detail] = plan;
                plan.NextWorkbenchIndex = 0; 
            }

            while (plans.Any(plan => plan.NextWorkbench != null))
            {
                var orderedPlans = plansWithIndex.Where(plan => plan.Plan.NextWorkbench != null)
                    .OrderBy(indexedPlan => priorities[indexedPlan.Plan.NextWorkbenchIndex][indexedPlan.Index])
                    .Select(indexedPlan => indexedPlan.Plan);

                foreach (var plan in orderedPlans)
                {
                    if(plan.NextWorkbench.IsBusy)
                        continue;
                    var duration = plan.NextWorkbench.CreateOrder(plan.Detail);
                    var timestamp = new WorkbenchTimestamp
                    {
                        Detail =  plan.Detail,
                        Duration =  duration,
                        Time = totalTime
                    };

                    timeStamps[plan.NextWorkbench].Add(timestamp);
                }

                var waitTime = factories.Where(factory => factory.OrderTime > 0).DefaultIfEmpty().Min(factory => factory?.OrderTime ?? 0);
                totalTime += waitTime;

                foreach (var factory in factories)
                {
                    var readyDetai = factory.Tick(waitTime);
                    if (readyDetai != null)
                        detailToPlan[readyDetai].NextWorkbenchIndex++;
                }
            }

            return totalTime;
        }

        public class DetailWorkplan
        {
            public DetailWorkplan(Detail detail)
            {
                Detail = detail;
                PlanList = new List<Workbench>(detail.OrdersDictionary.Keys);
                PlanList.Sort();
            }

            public DetailWorkplan(DetailWorkplan plan)
            {
                Detail = plan.Detail;
                PlanList = new List<Workbench>(plan.PlanList);
            }

            public Detail Detail { get; }
            public List<Workbench> PlanList { get; }
            public int NextWorkbenchIndex { get; set; }

            public Workbench NextWorkbench => NextWorkbenchIndex < PlanList.Count ? PlanList[NextWorkbenchIndex] : null;
        }

        public struct WorkbenchTimestamp
        {
            public int Time { get; set; }
            public int Duration { get; set; }
            public Detail Detail { get; set; }

            public override string ToString()
            {
                return $"Деталь {Detail} ({Time} - {Time + Duration})";
            }
        }
    }
}
