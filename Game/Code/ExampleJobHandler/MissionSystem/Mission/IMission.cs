using System;
using MissionSystem.Task;

namespace MissionSystem.Mission
{
    public interface IMission
    {
        public Guid MissionId { get; init; }
        public ITask Task { get; init; }
        public bool IsComplete { get; set; }
        public DateTime? TimeToComplete { get; init; }
        public void CompleteMissionAsync();
        public void FailMissionAsync();
    }
}