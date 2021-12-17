using Sample.SignalR.Proximity.Toaster;
using Samples.Framework.WPF;
using SignalR.Proximity;
using System.Threading.Tasks;

namespace Samples.SignalR.Proximity
{
    public class ProfessorViewModel : ViewModelBase, IToastNotificationsContract
    {
        private readonly IProximityEndPointProvider _endPointProvider;
        public ProfessorViewModel(IProximityEndPointProvider endPointProvider)
        {
            this._endPointProvider = endPointProvider;

            _=Connect();

        }

        private async Task Connect()
        {
            var cnx = _endPointProvider.Connect<IToastNotificationsContract>();
            cnx.Client.Attach(this);
            await cnx.StartAsync();

            await cnx.Notifier.ToAll().NotifyAsync(t => t.ShowError(new ToasterRequest()
            {
                Title ="test"
            }));
        }

        public void ShowError(ToasterRequest request)
        {

        }

        public void ShowInformation(ToasterRequest request)
        {
        }

        public void ShowSuccess(ToasterRequest request)
        {
        }

        public void ShowWarning(ToasterRequest request)
        {
        }
    }
}
