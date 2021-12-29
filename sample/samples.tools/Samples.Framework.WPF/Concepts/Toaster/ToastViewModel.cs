using Prism.Commands;
using System; 
namespace Samples.Framework.WPF.Concepts.Toaster
{ 
    public class ToastViewModel : ViewModelBase
    {
        private readonly IToastableHost _host; 
        private string? _Title;
        private string? _User;
        private string? _Message;
        private ToastTypes _ToastType;
        public ToastViewModel(IToastableHost host, ToastInfo infos)
        {

            if (infos == null)
                throw new ArgumentNullException(nameof(infos));
            this._host = host??throw new ArgumentNullException(nameof(host));
            this.ClosedCommand = new DelegateCommand(CloasAction);
            this.ToastType = infos.Type;
            this.Title = infos.Title;
            this.User = infos.User;
            this.Message = infos.Message;
        }



        public DelegateCommand ClosedCommand { get; private set; }
        public string? User
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
        public string? Title
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
        public string? Message
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
    }
}
