using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PID_r.Core;

namespace PID_r.GUI.Viewers
{
    public class DetailViewer : AbsractViewer
    {
        private readonly Detail detail;

        public DetailViewer(Detail detail)
        {
            this.detail = detail;
            foreach (var pair in detail.OrdersDictionary)
            {
                WorkbenchDatas.Add(new WorkbenchData()
                {
                    Workbench = pair.Key,
                    Worktime = pair.Value
                });
            }
        }

        public DetailViewer()
        {
            detail = new Detail("Test Detail");
        }

        public string Name
        {
            get => detail.Type;
            set => detail.Type = value;
        }


        public ObservableCollection<WorkbenchData> WorkbenchDatas { get; set; } = new ObservableCollection<WorkbenchData>();

        public class WorkbenchData
        {
            public Workbench Workbench { get; set; }
            public int Worktime { get; set; }

            public override string ToString()
            {
                return Workbench.Type;
            }
        }

        public void FillDetail()
        {
            foreach (var data in WorkbenchDatas)
            {
                detail.SetOrderTime(data.Workbench, data.Worktime);
            }
        }
    }
}