using Samples.Framework.WPF;

namespace Samples.SignalR.Proximity.Professor.Wpf
{
    public class GlobalViewModel : ViewModelBase
    {
        private readonly ProfessorViewModel _ProfessorViewModel;
        public GlobalViewModel(ProfessorViewModel pro)
        {
            _ProfessorViewModel = pro;


        }

        public ProfessorViewModel Pro => this._ProfessorViewModel;
    }
}
