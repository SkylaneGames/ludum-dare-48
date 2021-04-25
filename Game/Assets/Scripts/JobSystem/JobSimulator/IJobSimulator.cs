using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MissionSystem.JobSystem;
using MissionSystem.Mission;
using MissionSystem.Task;

namespace MissionSystem.JobSimulator
{
    public interface IJobSimulator
    {
        public Dictionary<Guid, IMission> GetCurrentMissions();
        public void CompleteMission(Guid missionId);
        public IMission AddNewMission(Item item, float timeToComplete);
        public IMission AddNewMission(Item item);
        public Dictionary<Guid, IMission> RemoveMission(Guid missionId);
        public void UpdateMissions(float deltaTime);
    }
}