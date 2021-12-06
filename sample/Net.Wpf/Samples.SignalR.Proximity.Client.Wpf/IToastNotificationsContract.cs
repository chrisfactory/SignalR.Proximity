
namespace Sample.SignalR.Proximity.Toaster
{
    public class ToasterRequest
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string FromUser { get; set; }
    }
    public interface IToastNotificationsContract
    {
        void ShowInformation(ToasterRequest request);
        void ShowSuccess(ToasterRequest request);
        void ShowWarning(ToasterRequest request);
        void ShowError(ToasterRequest request);
    }
}
