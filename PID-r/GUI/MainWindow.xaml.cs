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

        private void WorkbenchListDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var editWindow = new WorkbenchWindow((Workbench) WorkbenchList.SelectedItem);
            editWindow.ShowDialog();
            WorkbenchList.Items.Refresh();
        }

        private void CreateDetail(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void CreateWorkbecnh(object sender, RoutedEventArgs e)
        {
            var workbench = new Workbench("Empty Workbench");
            var editWindow = new WorkbenchWindow(workbench);
            if (editWindow.ShowDialog() == true)
            {
                ((MainWindowsViewer)DataContext).Workbenches.Add(workbench);
            }
        }
    }
}
