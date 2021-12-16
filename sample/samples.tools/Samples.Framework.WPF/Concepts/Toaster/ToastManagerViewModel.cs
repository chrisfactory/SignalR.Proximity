using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Windows;

namespace Samples.Framework.WPF.Concepts.Toaster
{ 
    public class ToastManagerViewModel : ViewModelBase, IToastableHost
    { 
        public IReadOnlyCollection<ToastViewModel> _ReadOnlyToasts;
        private ObservableCollection<ToastViewModel> _writableToasts = new ObservableCollection<ToastViewModel>();
        private Visibility _ManagerVisibility;
        private Visibility _CloseAllVisibility;
        private object _sync = new object();
        public ToastManagerViewModel()
        {
            _ReadOnlyToasts = _writableToasts;
            this.RefreshVisibilityStates();
            CloseAll = new DelegateCommand(CloseAllAction);
            base.RegisterCommands(CloseAll);
        }


        public DelegateCommand CloseAll { get; private set; }

        public Visibility ManagerVisibility
        {
            get { return _ManagerVisibility; }
            private set
            {
                if (_ManagerVisibility != value)
                {
                    _ManagerVisibility = value;
                    this.Notify();
                }
            }
        }
        public Visibility CloseAllVisibility
        {
            get { return _CloseAllVisibility; }
            private set
            {
                if (_CloseAllVisibility != value)
                {
                    _CloseAllVisibility = value;
                    this.Notify();
                }
            }
        }

        public IReadOnlyCollection<ToastViewModel> Toasts
        {
            get { return _ReadOnlyToasts; }
            private set
            {
                if (_ReadOnlyToasts != value)
                {
                    _ReadOnlyToasts = value;
                    this.Notify();
                }
            }
        }


        private void CloseAllAction()
        {
            lock (_sync)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _writableToasts.Clear();
                    this.RefreshVisibilityStates();
                }));
            }
        }

        public void AddToast(ToastInfo info)
        {
            lock (_sync)
            {

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this._writableToasts.Add(new ToastViewModel(this, info));
                    this.RefreshVisibilityStates();
                }));
            }
        }
        void IToastableHost.OnClosed(ToastViewModel item)
        {
            lock (_sync)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this._writableToasts.Remove(item);
                    this.RefreshVisibilityStates();
                }));
            }
        }


        private void RefreshVisibilityStates()
        {
            var count = Toasts.Count;
            if (count > 0)
            {
                ManagerVisibility = Visibility.Visible;
                if (count > 2)
                {
                    CloseAllVisibility = Visibility.Visible;
                }
                else
                    CloseAllVisibility = Visibility.Collapsed;
            }
            else
            {
                ManagerVisibility = Visibility.Collapsed;
            }

        } 
    }
}
