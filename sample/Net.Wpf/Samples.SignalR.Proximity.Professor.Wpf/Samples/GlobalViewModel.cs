using Samples.Framework.WPF;

namespace Samples.SignalR.Proximity.Professor.Wpf
{
    public class GlobalViewModel : ViewModelBase
    { 
        public GlobalViewModel(ProfessorViewModel professor)
        {
            Professor = professor;  
        }

        public ProfessorViewModel Professor { get; private set; }
    }
}
