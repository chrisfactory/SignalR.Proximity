using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Framework.WPF.Concepts.SampleStep
{
    public class SampleStepManager : ViewModelBase
    {
        public SampleStepManager(IEnumerable<ISampleStep> steps)
        {
            StepsTems = new ObservableCollection<ISampleStep>(steps);
        }

        public ObservableCollection<ISampleStep> StepsTems { get;  }
    }
}
