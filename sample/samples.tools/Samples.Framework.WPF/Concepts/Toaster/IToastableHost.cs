namespace Samples.Framework.WPF.Concepts.Toaster
{
    public interface IToastableHost
    {
        void OnClosed(ToastViewModel item);
    }
}
