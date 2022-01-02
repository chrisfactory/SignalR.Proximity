using Prism.Commands;
using Sample.SignalR.Proximity.Toaster;
using Samples.Framework.WPF;
using SignalR.Proximity;

namespace Samples.SignalR.Proximity
{
    public class StudentViewModel : UserViewModel, ISchoolContract
    {
        private readonly IConnection<ISchoolContract> _SchoolMessageConnection;
        public StudentViewModel(IProximityEndPointProvider endPointProvider, User user) : base(user)
        {
            SendToUsersCommand = new DelegateCommand(SendToUsersAction);


            _SchoolMessageConnection = endPointProvider.Connect<ISchoolContract>(cnxOptions =>
            {
                cnxOptions.Headers.Add("username", Name);
            });

            _SchoolMessageConnection.Client.Attach(this);
            _ = _SchoolMessageConnection.StartAsync();
        }


        public DelegateCommand SendToUsersCommand { get; set; }



        private async void SendToUsersAction()
        {
            await _SchoolMessageConnection.Notifier.ToUsers("Professor").NotifyAsync(t => t.Send("hello", Name));
        }


        //Receive message (callback)
        public void Send(string message, string from)
        {
            base.SetMessage(message, from);
        }
    }
}
