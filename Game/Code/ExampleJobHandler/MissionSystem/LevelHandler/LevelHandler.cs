using System;
using System.Collections.ObjectModel;
using System.Linq;
using MissionSystem.JobSimulator;
using MissionSystem.Task;

namespace MissionSystem.LevelHandler
{
    public class LevelHandler: ILevelHandler
    {
        public event StartLevelEvent StartLevelEvent;
        private IJobSimulator _jobSimulator = new JobSimulator.JobSimulator();
        private readonly Collection<ITask> _tasks = new ();
        private readonly Random _random;
        public const int MaxTimeToCompleteTaskSeconds = 30;
        public const int MinTasks = 1;
        public const int MaxTasks = 7;

        public LevelHandler()
        {
            // TODO Add missions via unit (I guess constructor?) rather than here.
            _tasks.Add(new MopStain());
            _tasks.Add(new StackShelves());
            _random = new Random();
        }

        public void UpdateLevel()
        {
            var missions = _jobSimulator.GetCurrentMissions();
            foreach (var keyValuePair in missions.Where(keyValuePair => keyValuePair.Value.TimeToComplete.HasValue && keyValuePair.Value.IsComplete == false).Where(keyValuePair => DateTime.Now > keyValuePair.Value.TimeToComplete))
            {
                _jobSimulator.RemoveMission(keyValuePair.Key);
            }
            if (_jobSimulator.GetCurrentMissions().Count < _random.Next(MinTasks, MaxTasks))
            {
                _jobSimulator.AddNewMission(_tasks[_random.Next(0, _tasks.Count)], _random.Next(0, MaxTimeToCompleteTaskSeconds));
            }
        }
        public void ResetLevel()
        {
            StartLevel();
        }
        public void StartLevel()
        {
            _jobSimulator = new JobSimulator.JobSimulator();
            StartLevelEvent?.Invoke(this, EventArgs.Empty);
            _jobSimulator.AddNewMission(new MopStain(), 0);
        }
    }
}