using Prism.Commands;
using Sample.SignalR.Proximity.Toaster;
using Samples.Framework.WPF;
using SignalR.Proximity; 
using System.Collections.Generic;
using System.Linq;

namespace Samples.SignalR.Proximity
{
    public class ProfessorViewModel : UserViewModel, ISchoolContract
    {
        private readonly IConnection<ISchoolContract> _SchoolMessageConnection;
        public ProfessorViewModel(IProximityEndPointProvider endPointProvider, User user) : base(user)
        {
            InitializeSample();

            _SchoolMessageConnection = endPointProvider.Connect<ISchoolContract>(cnxOptions =>
            {
                cnxOptions.Headers.Add("username", Name);//Do not use it in production. Used to simulate an implementation of user authentication. 
            });

            _SchoolMessageConnection.Client.Attach(this);//register for receive callback

            _ = _SchoolMessageConnection.StartAsync();

        }

        private async void SendToAllAction()
        {
            await _SchoolMessageConnection.Notifier.ToAll().NotifyAsync(t => t.Send("Send To All", Name));
        }

        private async void SendToOthersAction()
        {
            await _SchoolMessageConnection.Notifier.ToOthers().NotifyAsync(t => t.Send("Send To Others", Name));
        }

        private async void SendToUsersAction()
        {
            var targetUsers = TargetUsers.Where(u => u.IsSelected).Select(u => u.Name).ToArray();
            string userMessage = string.Empty;
            foreach (var user in targetUsers)
                userMessage += $"{user}; ";
            await _SchoolMessageConnection.Notifier.ToUsers(targetUsers).NotifyAsync(t => t.Send($"Send To Users: [ {userMessage}]", Name));
        }

        private async void SendToGroupsAction()
        {
            var targetGroups = TargetGroups.Where(u => u.IsSelected).Select(u => u.Name).ToArray();
            await _SchoolMessageConnection.Notifier.ToGroups(targetGroups).NotifyAsync(t => t.Send("hello", Name));
        }

        //Receive message (callback)
        public void Send(string message, string from)
        {
            base.SetMessage(message, from);
        }



        #region Sample
        private void InitializeSample()
        {
            SendToAllCommand = new DelegateCommand(SendToAllAction);
            SendToOthersCommand = new DelegateCommand(SendToOthersAction);
            SendToUsersCommand = new DelegateCommand(SendToUsersAction);
            SendToGroupsCommand = new DelegateCommand(SendToGroupsAction);
            TargetUsers = UsersProvider.Users.Select(u => new SelectedItem(u.Name, u)).ToList();
            foreach (SelectedItem usr in TargetUsers.Take(2))
                usr.IsSelected = true;
            TargetGroups = (new string[] { "group1", "group2" }).Select(u => new SelectedItem(u, u)).ToList();
        }

        public DelegateCommand SendToAllCommand { get; private set; }
        public DelegateCommand SendToOthersCommand { get; private set; }
        public DelegateCommand SendToUsersCommand { get; private set; }
        public DelegateCommand SendToGroupsCommand { get; private set; }
        public List<SelectedItem> TargetUsers { get; private set; }
        public List<SelectedItem> TargetGroups { get; private set; }
        #endregion
    }
}
