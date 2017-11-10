using System.Windows;
using PID_r.Core;
using PID_r.GUI.Viewers;

namespace PID_r.GUI
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Логика взаимодействия для DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow
    {
        private readonly DetailViewer viewer;

        public DetailWindow(Detail detail)
        {
            InitializeComponent();

            viewer = new DetailViewer(detail);
            DataContext = viewer;
        }

        private void OKClicked(object sender, RoutedEventArgs e)
        {
            viewer.FillDetail();
            DialogResult = true;
        }
    }
}
