using Samples.Framework.WPF;
using Samples.Framework.WPF.Concepts.SampleStep;

namespace Samples.SignalR.Proximity.Professor.Wpf
{
    public class GlobalViewModel : ViewModelBase
    {
        private readonly SampleStepManager _stepManager;
        private readonly ProfessorViewModel _ProfessorViewModel;
        public GlobalViewModel(SampleStepManager stepManager, ProfessorViewModel pro)
        {
            this._stepManager = stepManager;
            _ProfessorViewModel = pro;


        }


        public SampleStepManager StepManager => this._stepManager;


        public ProfessorViewModel Pro => this._ProfessorViewModel;
    }
}
