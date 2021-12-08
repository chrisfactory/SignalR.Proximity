using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SignalR.Proximity.Notifier;
using Sample.SignalR.Proximity.Toaster;
using SignalR.Proximity.Common;

namespace Samples.SignalR.Proximity.Notifier.Wpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _Title = "Exemple de Titre..";
        private string _Message = "Message...";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        public string MessageTitle
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    this.Notify();
                }
            }
        }

        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    this.Notify();
                }
            }
        }

        public const string TOASTER_GROUP = "GROUP_TEST";

        private void SendInformation(object sender, RoutedEventArgs e)
        {
            var notifier = SignalRProximityNotifierFactory.Create<IToastNotificationsContract>()
                .UseConfiguration(c =>
                    {
                        c.WithUrl("https://localhost:5031");
                        c.WithUserProvider(u =>
                        {
                            u.UserId = "Cohl";
                        });
                    })
              //.UseScopeUsers("chris")
              .UseScopeGroups(TOASTER_GROUP)
                .CallAsync(c =>
                       c.ShowInformation(new ToasterRequest()
                       {
                           FromUser = "MOI",
                           Message = Message,
                           Title = MessageTitle
                       }))
                ;


        }

        private void SendSuccess(object sender, RoutedEventArgs e)
        {
            var notifier = SignalRProximityNotifierFactory.Create<IToastNotificationsContract>()
              .UseScopeUsers("cohl")
                //.UseScopeGroups("Samples.Group.name")
                .CallAsync(c =>
                        c.ShowSuccess(new ToasterRequest()
                        {
                            FromUser = "MOI",
                            Message = Message,
                            Title = MessageTitle
                        }))
                ;
        }

        private void SenWarning(object sender, RoutedEventArgs e)
        {
            var notifier = SignalRProximityNotifierFactory.Create<IToastNotificationsContract>()
                //.UseScopeGroups("Samples.Group.name")
                .CallAsync(c =>
                       c.ShowWarning(new ToasterRequest()
                       {
                           FromUser = "MOI",
                           Message = Message,
                           Title = MessageTitle
                       }))
                ;
        }

        private void SenError(object sender, RoutedEventArgs e)
        {
            var notifier = SignalRProximityNotifierFactory.Create<IToastNotificationsContract>()
                //.UseScopeGroups("Samples.Group.name")
                .CallAsync(c =>
                     c.ShowError(new ToasterRequest()
                     {
                         FromUser = "MOI",
                         Message = Message,
                         Title = MessageTitle
                     }))
                ;
        }



        #region INotifyPropertyChanged
        private void Notify([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

}
