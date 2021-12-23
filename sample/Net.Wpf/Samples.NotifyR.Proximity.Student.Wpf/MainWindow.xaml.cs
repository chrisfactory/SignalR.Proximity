using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;  

namespace Samples.SignalR.Proximity.Student.Wpf
{
    public partial class MainWindow : Window
    {

        public MainWindow(GlobalViewModel globalViewModel)
        {
            InitializeComponent();
            DataContext = globalViewModel;
        }
    }
}
