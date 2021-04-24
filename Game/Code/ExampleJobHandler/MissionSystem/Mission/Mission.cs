using System;
using MissionSystem.Task;

namespace MissionSystem.Mission
{
    public class Mission: IMission
    {
        public event MissionCompletedEvent MissionCompletedEvent;
        public event MissionFailedEvent MissionFailedEvent;
        public Guid MissionId { get; init; }
        public ITask Task { get; init; }
        public bool IsComplete { get; set; }
        public DateTime? TimeToComplete { get; init; }
        public void CompleteMissionAsync()
        {
            IsComplete = true;
            MissionCompletedEvent?.Invoke(this, EventArgs.Empty);
        }
        
        public void FailMissionAsync()
        {
            MissionFailedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}