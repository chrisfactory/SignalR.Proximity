using NotifyR.Notifier;
using NotifyR.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Samples.NotifyR.Notifier.WpfNetCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        { 
            InitializeComponent();

            Load();
        }
        private void Load()
        {

            //var notifier = NotifyRNotifierFactory.Get().New<IToaster>().WithUser("cohl").UseScopeUser("cohl");//.CallAsync(t =>
          
            //using (var context =   notifier.GetProxy())
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        context.Proxy.Notify();
            //    }
            //}

            //using (var context =   notifier.GetProxy())
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        context.Proxy.Notify();
            //    }
            //}

        }
    }
}
