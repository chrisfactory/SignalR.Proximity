using Samples.Framework.WPF;
using Samples.Framework.WPF.Concepts.SampleStep;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Samples.SignalR.Proximity.Student.Wpf
{
    public class GlobalViewModel : ViewModelBase
    {
        public GlobalViewModel(IEnumerable<StudentViewModel> students)
        {
            Students = new ObservableCollection<StudentViewModel>(students); 
        }

         

        public ObservableCollection<StudentViewModel> Students { get; }
    }
}
