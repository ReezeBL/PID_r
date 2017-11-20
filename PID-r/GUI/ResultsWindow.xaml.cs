using System;
using PID_r.GUI.Viewers;

namespace PID_r.GUI
{
    /// <summary>
    /// Логика взаимодействия для ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow
    {
        private readonly ResultsViewer viewer;

        public ResultsWindow(string results, int minTime, int maxTime)
        {
            InitializeComponent();

            viewer = new ResultsViewer
            {
                Results = results,
                MinTime = minTime,
                MaxTime = maxTime
            };

            DataContext = viewer;
        }
    }
}
