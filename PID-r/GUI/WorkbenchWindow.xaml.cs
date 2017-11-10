using System.Windows;
using PID_r.Core;
using PID_r.GUI.Viewers;

namespace PID_r.GUI
{
    /// <summary>
    /// Логика взаимодействия для WorkbenchWindow.xaml
    /// </summary>
    public partial class WorkbenchWindow
    {
        public WorkbenchWindow(Workbench workbench)
        {
            InitializeComponent();
            DataContext = workbench;
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
