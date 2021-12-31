using Prism.Commands;
using Sample.SignalR.Proximity.Toaster;
using Samples.Framework.WPF;
using SignalR.Proximity;

namespace Samples.SignalR.Proximity
{
    public class StudentViewModel : UserViewModel, ISchoolContract
    {
        private readonly IConnection<ISchoolContract> _SchoolMessageConnection;
        public StudentViewModel(IProximityEndPointProvider endPointProvider, string name) : base(name)
        {
            SendCommand = new DelegateCommand(SendAction);


            _SchoolMessageConnection = endPointProvider.Connect<ISchoolContract>(cnxOptions =>
            {
                cnxOptions.Headers.Add("username", Name);
            });

            _SchoolMessageConnection.Client.Attach(this);
            _ = _SchoolMessageConnection.StartAsync();
        }


        public DelegateCommand SendCommand { get; set; }



        private async void SendAction()
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
