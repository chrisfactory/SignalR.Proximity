using System;
using SignalR.Proximity;

namespace Sample.SignalR.Proximity.Toaster
{
    public class ToasterRequest
    {
        public Guid? CorrelationKey { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string FromUser { get; set; }
    }

    [ProximityTypeScriptCodeSync($"../../sample-react/src/contracts.{nameof(IToastNotificationsContract)}.ts")]
    public interface IToastNotificationsContract
    {
        void ShowInformation(ToasterRequest request);
        void ShowSuccess(ToasterRequest request);
        void ShowWarning(ToasterRequest request);
        void ShowError(ToasterRequest request);
    }
}
