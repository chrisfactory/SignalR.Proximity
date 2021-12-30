using Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Samples.Framework.WPF
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private PropertyChangedEventHandler? _PropertyChanged;
        private readonly List<DelegateCommandBase> _commands = new();


        #region DelegateCommand
        protected void RegisterCommands(params DelegateCommandBase[] commands)
        {
            if (commands == null)
                return; 
            foreach (var cmd in commands) 
                _commands.Add(cmd); 
        }

        public void RaiseCanExecuteCommands()
        {
            foreach (var cmd in _commands) 
                cmd.RaiseCanExecuteChanged(); 
        }
        #endregion

        #region NotifyPropertyChanged
        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged { add { _PropertyChanged+=value; } remove { _PropertyChanged-=value; } }

        protected void Notify([CallerMemberName] string? propertyName = null)
        {
            _PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged(string? propertyName) { }
        #endregion

    }
}
