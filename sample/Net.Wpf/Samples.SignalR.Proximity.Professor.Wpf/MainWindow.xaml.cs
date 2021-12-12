using Samples.Ui;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Sample.SignalR.Proximity.Toaster;
using SignalR.Proximity;

namespace Samples.SignalR.Proximity.Professor.Wpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged, IToastNotificationsContract
    {
        protected readonly IProximityEndPointProvider _proximityProvider;

        public MainWindow(IProximityEndPointProvider proximityProvider)
        {
            InitializeComponent();

            _proximityProvider = proximityProvider;
            var cnx = _proximityProvider.Connect<IToastNotificationsContract>(

                );

            cnx.Client.Attach(this);

            cnx.StartAsync().Wait();
            _ = cnx.Client.JoinGroupsAsync("S1", "S2");
            _ = cnx.Client.QuitGroupsAsync("S1", "S2");
            cnx.Client.Dettach(this);
            //cnx.Client.DettachAll();



             
            ToastManager = new ToastManager();
            DataContext = this;
        }

        public ToastManager ToastManager { get; set; }


        void IToastNotificationsContract.ShowError(ToasterRequest request)
        {
            this.ToastManager.AddToast(new ToastInfo()
            {
                Message = request.Message,
                Title = request.Title,
                User = request.FromUser,
                Type = ToastTypes.Error
            });
        }

        void IToastNotificationsContract.ShowInformation(ToasterRequest request)
        {
            this.ToastManager.AddToast(new ToastInfo()
            {
                Message = request.Message,
                Title = request.Title,
                User = request.FromUser,
                Type = ToastTypes.Information
            });
        }

        void IToastNotificationsContract.ShowSuccess(ToasterRequest request)
        {
            this.ToastManager.AddToast(new ToastInfo()
            {
                Message = request.Message,
                Title = request.Title,
                User = request.FromUser,
                Type = ToastTypes.Success
            });
        }

        void IToastNotificationsContract.ShowWarning(ToasterRequest request)
        {
            this.ToastManager.AddToast(new ToastInfo()
            {
                Message = request.Message,
                Title = request.Title,
                User = request.FromUser,
                Type = ToastTypes.Warning
            });
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
