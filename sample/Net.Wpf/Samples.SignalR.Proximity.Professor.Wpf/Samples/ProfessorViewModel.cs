using Prism.Commands;
using Sample.SignalR.Proximity.Toaster;
using Samples.Framework.WPF;
using SignalR.Proximity;

namespace Samples.SignalR.Proximity
{
    public class ProfessorViewModel : ViewModelBase, ISchoolContract
    {
        private readonly IConnection<ISchoolContract> _SchoolMessageConnection;
        public ProfessorViewModel(IProximityEndPointProvider endPointProvider)
        {
            Name = "Professor";
            SendCommand = new DelegateCommand(SendAction);

            _SchoolMessageConnection = endPointProvider.Connect<ISchoolContract>(cnxOptions =>
             {
                 cnxOptions.Headers.Add("username", Name);
             });

            _SchoolMessageConnection.Client.Attach(this);

            _ = _SchoolMessageConnection.StartAsync();
        }


        public string Name { get; }
        public DelegateCommand SendCommand { get; set; }

        private async void SendAction()
        {
            await _SchoolMessageConnection.Notifier.ToAll().NotifyAsync(t => t.Send("hello", Name));
        }


        //Receive message (callback)
        public void Send(string message, string from)
        {

        }
    }
}
