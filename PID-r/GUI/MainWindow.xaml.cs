using System.Text;
using System.Windows;
using System.Windows.Input;
using PID_r.Core;
using PID_r.GUI.Viewers;

namespace PID_r.GUI
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditWorkbench(object sender, MouseButtonEventArgs e)
        {
            var editWindow = new WorkbenchWindow((Workbench) WorkbenchList.SelectedItem);
            editWindow.ShowDialog();
            WorkbenchList.Items.Refresh();
        }

        private void CreateDetail(object sender, RoutedEventArgs e)
        {
            var detail = new Detail("Empty Detail");
            var editWindow = new DetailWindow(detail);
            if (editWindow.ShowDialog() == true)
            {
                MainWindowsViewer.Details.Add(detail);
            }
        }

        

        private void CreateWorkbecnh(object sender, RoutedEventArgs e)
        {
            var workbench = new Workbench("Empty Workbench");
            var editWindow = new WorkbenchWindow(workbench);
            if (editWindow.ShowDialog() == true)
            {
                MainWindowsViewer.Workbenches.Add(workbench);
            }
        }

        private void EditDetail(object sender, MouseButtonEventArgs e)
        {
            var editWindow = new DetailWindow((Detail)DetailList.SelectedItem);
            editWindow.ShowDialog();
            DetailList.Items.Refresh();
        }

        private void Simulate(object sender, RoutedEventArgs e)
        {
            var optPlan = Factory.Simulate(MainWindowsViewer.Details, out int minTime, out int maxTime);
            var stringBuilder = new StringBuilder(1024);

            //stringBuilder.AppendLine($"Minimal time: {minTime}");
            //stringBuilder.AppendLine($"Max time: {maxTime}");
            //stringBuilder.AppendLine($"Optimal plan:");
            foreach (var detailWorkplan in optPlan)
            {
                stringBuilder.Append($"Станок {detailWorkplan.Key}: ");
                stringBuilder.AppendLine(string.Join(" => ", detailWorkplan.Value));
            }

            var resultsForm = new ResultsWindow(stringBuilder.ToString(), minTime, maxTime);
            resultsForm.ShowDialog();
        }
    }
}
