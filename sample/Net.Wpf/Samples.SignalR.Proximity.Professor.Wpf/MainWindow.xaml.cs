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
        protected readonly IProximityProvider _proximityProvider;

        public MainWindow(IProximityProvider proximityProvider, IProximityContext defaultContext)
        {
            InitializeComponent();

            _proximityProvider = proximityProvider;


            var builderFromCode = _proximityProvider.Get("From.Code");
            var builderFromConfigFile = _proximityProvider.Get("From.ConfigFile");


            var cl = builderFromCode.Client<IToastNotificationsContract>();
            cl.WithGroups("c1").AttachStart(this);
            cl.AttachStart(this);
            //   toasterClient.Noti
            var A = builderFromCode.Notifier<IToastNotificationsContract>().UseScopeOthers();
           _= A.CallAsync(c => c.ShowError(new ToasterRequest()
            {
                FromUser = "Professor",
                Message ="MassaA"
            }));
            _= A.CallAsync(c => c.ShowError(new ToasterRequest()
            {
                FromUser = "Professor",
                Message ="MassaB"
            }));
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
