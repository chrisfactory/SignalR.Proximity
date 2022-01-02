using System.Windows;

namespace Samples.SignalR.Proximity.Student.Wpf
{
    public partial class MainWindow : Window
    {

        public MainWindow(GlobalViewModel globalViewModel)
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height -50;
            DataContext = globalViewModel;
        }
    }
}
