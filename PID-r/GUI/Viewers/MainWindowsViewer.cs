using System.Collections.ObjectModel;
using PID_r.Core;

namespace PID_r.GUI.Viewers
{
    public class MainWindowsViewer : AbsractViewer
    {
        public static ObservableCollection<Detail> Details { get; } = new ObservableCollection<Detail> {new Detail("GOVNO 123213213213")};
        public static ObservableCollection<Workbench> Workbenches { get; } = new ObservableCollection<Workbench>{ new Workbench("ZALUPA")};
    }
}