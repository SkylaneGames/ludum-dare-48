using System;
using MissionSystem.Task;
using UnityEngine;

namespace MissionSystem.Mission
{
    public class Mission : IMission
    {
        public event MissionCompletedEvent MissionCompletedEvent;
        public event MissionFailedEvent MissionFailedEvent;
        public Guid MissionId { get; set; }
        public Item Item { get; set; }
        public bool IsComplete { get; set; }
        public float? TimeToComplete { get; set; }
        public float? TimeRemaining { get; set; }

        public float? NormalisedTimeRemaing
        {
            get
            {
                if (TimeRemaining.HasValue && TimeToComplete.HasValue)
                {
                    return Mathf.Clamp01(TimeRemaining.Value / TimeToComplete.Value);
                }

                return null;
            }
        }

        public Mission()
        {

        }

        public Mission(float timeToComplete)
        {
            TimeToComplete = timeToComplete;
            TimeRemaining = timeToComplete;
        }

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