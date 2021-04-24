using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MissionSystem.Mission;
using MissionSystem.Task;

namespace MissionSystem.JobSimulator
{
    public interface IJobSimulator
    {
        public Dictionary<Guid, IMission> GetCurrentMissions();
        public void CompleteMission(Guid missionId);
        public Task<IMission> AddNewMission(ITask taskToComplete, int timeToComplete);
        public Task<IMission> AddNewMission(ITask taskToComplete);
        public Dictionary<Guid, IMission> RemoveMission(Guid missionId);
    }
}