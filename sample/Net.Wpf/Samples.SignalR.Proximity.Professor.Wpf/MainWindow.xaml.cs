using Samples.Ui;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Sample.SignalR.Proximity.Toaster;
using SignalR.Proximity;
using System.Threading.Tasks;

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

            _ = Test();

            ToastManager = new ToastManager();
            DataContext = this;
        }



        private async Task Test()
        {

            var cnx = _proximityProvider.Connect<IToastNotificationsContract>();
            await cnx.StartAsync();

            cnx.Client.Attach(this);
            await cnx.Client.JoinGroupsAsync("S1", "S2");


            await cnx.Notifier.ToAll().Notify(toaster => toaster.ShowError(new ToasterRequest() { FromUser = "my", Message = "ToAll", Title = "title test" }));
            await cnx.Notifier.ToOthers().Notify(toaster => toaster.ShowError(new ToasterRequest() { FromUser = "my", Message = "ToOthers", Title = "title test" }));

            await cnx.Notifier.ToClients(cnx.ConnectionId).Notify(toaster => toaster.ShowInformation(new ToasterRequest() { FromUser = "my", Message = $"ToClients {cnx.ConnectionId}", Title = "title test" }));
            await cnx.Notifier.ToAllExcept(cnx.ConnectionId).Notify(toaster => toaster.ShowInformation(new ToasterRequest() { FromUser = "my", Message = $"ToAllExcept {cnx.ConnectionId}", Title = "title test" }));
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
