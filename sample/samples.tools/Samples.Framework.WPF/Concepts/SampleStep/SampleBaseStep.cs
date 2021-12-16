namespace Samples.Framework.WPF.Concepts.SampleStep
{
    public interface ISampleStep
    {

    }
    public abstract class SampleBaseStep : ViewModelBase, ISampleStep
    {
        private string? _title;
        private string? _menuTitle;

        public SampleBaseStep()
        {

        }

         


        public string? Title
        {
            get { return this._title; }
            set
            {
                if (this._title != value)
                {
                    this._title = value;
                    base.Notify();
                }
            }
        }
        public string? MenuTitle
        {
            get { return this._menuTitle; }
            set
            {
                if (this._menuTitle != value)
                {
                    this._menuTitle = value;
                    base.Notify();
                }
            }
        }
    }
}
