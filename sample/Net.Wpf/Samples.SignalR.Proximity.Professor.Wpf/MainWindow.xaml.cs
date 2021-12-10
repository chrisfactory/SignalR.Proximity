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
            var c1 = builderFromCode.Client.WithGroups("C1", "C2");
            var c2 = builderFromCode.Notifier;


            var proxy = c1.WithGroups("C1", "C2").Build();





            ToastManager = new ToastManager();
            DataContext = this;



            //var TaskDeClientProxy = SignalRProximityClientFactory.Create<IToastNotificationsContract>()
            //    .UseConfiguration(c =>
            //                    {
            //                        c.WithGroupsAutoRestored();
            //                    })
            //    .WithGroups(TOASTER_GROUP)
            //    .AttachStartAsync(this);

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
