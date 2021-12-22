using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Samples.Framework.WPF.Concepts.SampleStep
{
    public class SampleStepManager : ViewModelBase
    {
        private ISampleStep? _CurrentStep;
        public SampleStepManager(IEnumerable<ISampleStep> steps)
        {
            StepsTems = new ObservableCollection<ISampleStep>(steps);
            CurrentStep= StepsTems.FirstOrDefault();
        }

        public ObservableCollection<ISampleStep> StepsTems { get; }

        public ISampleStep? CurrentStep
        {
            get { return _CurrentStep; }
            set
            {
                if (!ReferenceEquals(this._CurrentStep, value))
                {
                    _CurrentStep = value;
                    base.Notify();
                }
            }
        }
    }
}
