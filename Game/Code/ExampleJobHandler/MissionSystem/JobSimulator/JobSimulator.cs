using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using MissionSystem.Mission;
using MissionSystem.Task;

namespace MissionSystem.JobSimulator
{
    public class JobSimulator: IJobSimulator
    {
        private readonly Dictionary<Guid, IMission> _missions = new Dictionary<Guid, IMission>();

        public Dictionary<Guid, IMission> RemoveMission(Guid missionId)
        {
            _missions[missionId].FailMissionAsync();
            _missions.Remove(missionId);
            return GetCurrentMissions();
        }
        public Dictionary<Guid, IMission> GetCurrentMissions()
        {
            return _missions;
        }
        public void CompleteMission(Guid missionId)
        {
            _missions[missionId].CompleteMissionAsync();
        }
        public async Task<IMission> AddNewMission(ITask taskToComplete, int timeToComplete)
        {
            var timeToCompleteBy = DateTime.Now.AddSeconds(timeToComplete);
            var task = new Task<Mission.Mission>(() =>
                {
                    var mission = new Mission.Mission
                    {
                        Task = taskToComplete,
                        IsComplete = false,
                        MissionId = Guid.NewGuid(),
                        TimeToComplete = timeToCompleteBy
                    };
                    _missions.TryAdd(mission.MissionId, mission);
                    return mission;
                });
            return await task;
        }
        public async Task<IMission> AddNewMission(ITask taskToComplete)
        {
            var task = new Task<Mission.Mission>(() =>
                {
                    var mission = new Mission.Mission
                    {
                        Task = taskToComplete,
                        IsComplete = false,
                        MissionId = Guid.NewGuid()
                    };
                    _missions.TryAdd(mission.MissionId, mission);
                    return mission;
                });
            return await task;
        }
    }
}