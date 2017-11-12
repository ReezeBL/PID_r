using System.Collections.ObjectModel;
using PID_r.Core;

namespace PID_r.GUI.Viewers
{
    public class MainWindowsViewer : AbsractViewer
    {
        public static ObservableCollection<Detail> Details { get; } = new ObservableCollection<Detail> {new Detail("Template Detail")};
        public static ObservableCollection<Workbench> Workbenches { get; } = new ObservableCollection<Workbench>{ new Workbench("Template Workbench")};
    }
}