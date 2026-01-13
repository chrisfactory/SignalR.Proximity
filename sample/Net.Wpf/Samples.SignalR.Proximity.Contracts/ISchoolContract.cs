using SignalR.Proximity;
using System;
using System.Collections.Generic;

namespace Sample.SignalR.Proximity.Toaster
{
    [ProximityTypeScriptCodeSync($"../../sample-react/src/contracts.{nameof(ISchoolContract)}.ts")]
    public interface ISchoolContract
    {
        void Send(string message, string from);

        void OnWorkflowProgess(WorkflowProgess progresss);
    }

    public enum WorkflowProgessStatus
    {
        None= 0,
        Running = 1,
        Failed = 2,
        Succeeded = 3,
    }
    public class WorkflowProgess
    {
        public required Guid Key { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required WorkflowProgessStatus Status { get; set; }
        public required List<WorkflowStepProgress> Steps { get; set; }

    }
    public class WorkflowStepProgress
    {
        public required Guid Key { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required WorkflowProgessStatus Status { get; set; }

    }
}
