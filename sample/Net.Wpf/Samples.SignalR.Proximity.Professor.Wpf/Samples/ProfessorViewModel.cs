using Prism.Commands;
using Sample.SignalR.Proximity.Toaster;
using Samples.Framework.WPF;
using SignalR.Proximity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

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

        private async void SendWorkflowProgessAction()
        {
            var targetUsers = new List<string>() { "Professor", "Ryo" }.ToArray();
            var targetGroups = TargetGroups.Where(u => u.IsSelected).Select(u => u.Name).ToArray();
            await _SchoolMessageConnection.Notifier.ToUsers(targetUsers).NotifyAsync(t => t.OnWorkflowProgess(new WorkflowProgess()
            {
                Description = "test complex oject",
                Key = Guid.NewGuid(),
                Name = "Test OnWorkflowProgess",
                Status = WorkflowProgessStatus.Running,
                Steps = new List<WorkflowStepProgress>()
                {
                    new WorkflowStepProgress (){Name ="Step 1", Description ="Description 1", Key = Guid.NewGuid(), Status = WorkflowProgessStatus.Succeeded},
                    new WorkflowStepProgress (){Name ="Step 2", Description ="Description 2", Key = Guid.NewGuid(), Status = WorkflowProgessStatus.Running},
                    new WorkflowStepProgress (){Name ="Step 3", Description ="Description 3", Key = Guid.NewGuid(), Status = WorkflowProgessStatus.None}
                }
            }));
        }

        //Receive message (callback)
        public void Send(string message, string from)
        {
            base.SetMessage(message, from);
        }

        public void OnWorkflowProgess(WorkflowProgess progresss)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                Converters =
                                    {
                                        new JsonStringEnumConverter()
                                    }
            };
            var json = JsonSerializer.Serialize(progresss, options);
            MessageBox.Show($"Exemple progress:{json}");
        }

        #region Sample
        private void InitializeSample()
        {
            SendToAllCommand = new DelegateCommand(SendToAllAction);
            SendToOthersCommand = new DelegateCommand(SendToOthersAction);
            SendToUsersCommand = new DelegateCommand(SendToUsersAction);
            SendToGroupsCommand = new DelegateCommand(SendToGroupsAction);
            SendWorkflowProgessCommand = new DelegateCommand(SendWorkflowProgessAction);
            TargetUsers = UsersProvider.Users.Select(u => new SelectedItem(u.Name, u)).ToList();
            foreach (SelectedItem usr in TargetUsers.Take(2))
                usr.IsSelected = true;
            TargetGroups = (new string[] { "group1", "group2" }).Select(u => new SelectedItem(u, u)).ToList();
        }



        public DelegateCommand SendToAllCommand { get; private set; }
        public DelegateCommand SendToOthersCommand { get; private set; }
        public DelegateCommand SendToUsersCommand { get; private set; }
        public DelegateCommand SendToGroupsCommand { get; private set; }
        public DelegateCommand SendWorkflowProgessCommand { get; private set; }
        public List<SelectedItem> TargetUsers { get; private set; }
        public List<SelectedItem> TargetGroups { get; private set; }
        #endregion
    }
}
