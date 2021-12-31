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
            SendToAllCommand = new DelegateCommand(SendToAllAction);
            SendToOthersCommand = new DelegateCommand(SendToOthersAction);
            SendToUsersCommand = new DelegateCommand(SendToUsersAction);
            SendToGroupsCommand = new DelegateCommand(SendToGroupsAction);

            _SchoolMessageConnection = endPointProvider.Connect<ISchoolContract>(cnxOptions =>
             {
                 cnxOptions.Headers.Add("username", Name);//Don't do this in production. Used to simulate an implementation of user authentication. 
             });

            _SchoolMessageConnection.Client.Attach(this);//register for receive callback

            _ = _SchoolMessageConnection.StartAsync();

        }


        public string Name { get; set; }
        public DelegateCommand SendToAllCommand { get;private set; }
        public DelegateCommand SendToOthersCommand { get; private set; }
        public DelegateCommand SendToUsersCommand { get; private set; }
        public DelegateCommand SendToGroupsCommand { get; private set; }


        private async void SendToAllAction()
        {
            await _SchoolMessageConnection.Notifier.ToAll().NotifyAsync(t => t.Send("hello", Name));
        }

        private async void SendToOthersAction()
        {
            await _SchoolMessageConnection.Notifier.ToOthers().NotifyAsync(t => t.Send("hello", Name));
        }

        private async void SendToUsersAction()
        {
            await _SchoolMessageConnection.Notifier.ToUsers("...").NotifyAsync(t => t.Send("hello", Name));
        }

        private async void SendToGroupsAction()
        {
            await _SchoolMessageConnection.Notifier.ToGroups("...").NotifyAsync(t => t.Send("hello", Name));
        }

        //Receive message (callback)
        public void Send(string message, string from)
        {

        }
    }
}
