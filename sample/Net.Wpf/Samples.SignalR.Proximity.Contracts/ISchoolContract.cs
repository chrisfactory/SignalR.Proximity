using SignalR.Proximity;

namespace Sample.SignalR.Proximity.Toaster
{
    [ProximityTypeScriptCodeSync("../../sample-react/src/contracts.ISchoolContract.ts")]
    public interface ISchoolContract
    {
        void Send(string message, string from);
    }
}
