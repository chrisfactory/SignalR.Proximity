using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Samples.Ui
{
    public interface IToastHost
    {
        void OnClosed(ToastViewModel item);
    }
    public class ToastViewModel : INotifyPropertyChanged
    {
        private IToastHost _host;
        private PropertyChangedEventHandler _PropertyChanged;
        private string _Title;
        private string _User;
        private string _Message;
        private ToastTypes _ToastType;
        public ToastViewModel(IToastHost host, ToastInfo infos)
        {

            if (infos == null)
                throw new ArgumentNullException(nameof(infos));

            if (host == null)
                throw new ArgumentNullException(nameof(host));

            this._host = host;
            this.ClosedCommand = new DelegateCommand(CloasAction);
            this.ToastType = infos.Type;
            this.Title = infos.Title;
            this.User = infos.User;
            this.Message = infos.Message;
        }



        public DelegateCommand ClosedCommand { get; private set; }
        public string User
        {
            get { return _User; }
            private set
            {
                if (_User != value)
                {
                    _User = value;
                    this.Notify();
                }
            }
        }
        public string Title
        {
            get { return _Title; }
            private set
            {
                if (_Title != value)
                {
                    _Title = value;
                    this.Notify();
                }
            }
        }
        public string Message
        {
            get { return _Message; }
            private set
            {
                if (_Message != value)
                {
                    _Message = value;
                    this.Notify();
                }
            }
        }
        public ToastTypes ToastType
        {
            get { return _ToastType; }
            private set
            {
                if (_ToastType != value)
                {
                    _ToastType = value;
                    this.Notify();
                }
            }
        }

        private void CloasAction()
        {
            _host.OnClosed(this);
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged { add { _PropertyChanged += value; } remove { _PropertyChanged -= value; } }
        private void Notify([CallerMemberName]string propertyName = null)
        {
            var handler = _PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
