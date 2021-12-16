using Samples.Framework.WPF;
using Samples.Framework.WPF.Concepts.SampleStep;

namespace Samples.SignalR.Proximity.Professor.Wpf
{
    public class GlobalViewModel : ViewModelBase
    {
        private readonly SampleStepManager _stepManager;
        public GlobalViewModel(SampleStepManager stepManager)
        {
            this._stepManager = stepManager; 
        }


        public SampleStepManager StepManager => this._stepManager;
        
    }
}
