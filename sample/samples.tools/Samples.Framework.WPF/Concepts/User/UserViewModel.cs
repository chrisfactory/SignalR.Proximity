using System;
using System.Windows.Threading;

namespace Samples.Framework.WPF
{
    public class UserViewModel : ViewModelBase
    {
        private readonly DispatcherTimer timer = new();
        public UserViewModel(string name)
        {
            Name = name; 
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Tick;
        }



        public string Name { get; }

        private UserMessageViewModel? _UserMessage;
        public UserMessageViewModel? UserMessage
        {
            get { return _UserMessage; }
            private set
            {
                if (_UserMessage != value)
                {
                    _UserMessage = value;
                    base.Notify();
                }
            }
        }

        private void Tick(object? sender, EventArgs e)
        {
            timer.Stop();
            UserMessage = null;
        }
        protected override void OnPropertyChanged(string? propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(UserMessage) && UserMessage != null)
            {
                timer.Stop();
                timer.Start();
            }
        }
        protected void SetMessage(string? message, string? from)
        {
            UserMessage = new UserMessageViewModel(message, from); 
        }

    }
    public class UserMessageViewModel : ViewModelBase
    {
        public UserMessageViewModel(string? message, string? from)
        {
            Message = message;
            From = from;
        }
        public string? Message { get; }
        public string? From { get; }

    }
}
