using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MissionSystem.JobSystem;
using MissionSystem.Mission;
using MissionSystem.Task;

namespace MissionSystem.JobSimulator
{
    public class JobSimulator: IJobSimulator
    {
        private readonly Dictionary<Guid, IMission> _missions = new Dictionary<Guid, IMission>();

        public Dictionary<Guid, IMission> RemoveMission(Guid missionId)
        {
            _missions[missionId].FailMission();
            _missions.Remove(missionId);
            return GetCurrentMissions();
        }
        public Dictionary<Guid, IMission> GetCurrentMissions()
        {
            return _missions;
        }
        public void CompleteMission(Guid missionId)
        {
            _missions[missionId].CompleteMission();
            _missions.Remove(missionId);
        }
        public IMission AddNewMission(Item item, float timeToComplete)
        {
            var mission = new Mission.Mission(timeToComplete)
            {
                Item = item,
                IsComplete = false,
                MissionId = Guid.NewGuid(),
            };
            _missions.Add(mission.MissionId, mission);
            return mission;
        }

        public IMission AddNewMission(Item item)
        {
            var mission = new Mission.Mission()
            {
                Item = item,
                IsComplete = false,
                MissionId = Guid.NewGuid()
            };
            _missions.Add(mission.MissionId, mission);
            return mission;
        }

        public void UpdateMissions(float deltaTime)
        {
            for (int i = _missions.Count - 1; i >= 0; i--)
            {
                var mission = _missions.ToList()[i].Value;
                if (mission.TimeToComplete.HasValue && mission.TimeRemaining.HasValue)
                {
                    mission.TimeRemaining -= deltaTime;
                    if (mission.TimeRemaining <= 0)
                    {
                        RemoveMission(mission.MissionId);
                    }

                }
            }
        }
    }
}