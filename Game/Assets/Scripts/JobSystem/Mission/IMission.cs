using System;
using MissionSystem.Task;

namespace MissionSystem.Mission
{
    public interface IMission
    {
        event MissionCompletedEvent MissionCompletedEvent;
        event MissionFailedEvent MissionFailedEvent;

        public Guid MissionId { get; set; }
        public Item Item { get; set; }
        public bool IsComplete { get; set; }
        public float? TimeToComplete { get; set; }
        public float? TimeRemaining { get; set; }
        public float? NormalisedTimeRemaing { get; }
        public void CompleteMission();
        public void FailMission();
    }
}