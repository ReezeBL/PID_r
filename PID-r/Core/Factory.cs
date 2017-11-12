using System;
using System.Collections.Generic;
using System.Linq;
using PID_r.Helpers;

namespace PID_r.Core
{
    public static class Factory
    {

        public static IList<DetailWorkplan> Simulate(IEnumerable<Detail> details, out int minTime, out int maxTime)
        {
            var plans = details.Select(detail => new DetailWorkplan(detail)).ToList();
            minTime = int.MaxValue;
            maxTime = int.MinValue;

            List<DetailWorkplan> optimalPlan = null;

            do
            {
                var planTime = SimulatePlan(plans);
                if (planTime < minTime)
                {
                    minTime = planTime;
                    optimalPlan = new List<DetailWorkplan>(plans.Select(plan => new DetailWorkplan(plan)));
                }

                if (maxTime < planTime)
                    maxTime = planTime;

            } while (CombHelper.NextMultyPermutation(plans.Select(plan => plan.PlanList)));

            return optimalPlan;
        }

        private static int SimulatePlan(List<DetailWorkplan> plans)
        {
            var totalTime = 0;
            var detailToPlan = new Dictionary<Detail, DetailWorkplan>();
            var factories = new HashSet<Workbench>(plans.SelectMany(plan => plan.PlanList));

            foreach (var plan in plans)
            {
                detailToPlan[plan.Detail] = plan;
                plan.NextWorkbenchIndex = 0;
            }

            while (plans.Any(plan => plan.NextWorkbench != null))
            {
                foreach (var plan in plans)
                {
                    if(plan.NextWorkbench?.IsBusy ?? true)
                        continue;
                    plan.NextWorkbench.CreateOrder(plan.Detail);
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
    }
}
