using System;
using MissionSystem.Task;

namespace MissionSystem.Mission
{
    public interface IMission
    {
        public Guid MissionId { get; set; }
        public ITask Task { get; set; }
        public bool IsComplete { get; set; }
        public float? TimeToComplete { get; set; }
        public float? TimeRemaining { get; set; }
        public float? NormalisedTimeRemaing { get; }
        public void CompleteMissionAsync();
        public void FailMissionAsync();
    }
}