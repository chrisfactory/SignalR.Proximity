using System.Windows;
namespace Samples.SignalR.Proximity.Professor.Wpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(GlobalViewModel globalViewModel)
        {
            InitializeComponent();
            this.Left = 50;
            this.Top = 0;
            DataContext = globalViewModel;
        }
    }
}
