using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MissionSystem.JobSimulator;
using MissionSystem.Mission;
using MissionSystem.Task;
using UnityEngine;

namespace MissionSystem.JobSystem
{
    public delegate void JobCreated(IMission job);
    public delegate void JobRemoved(System.Guid jobId);

    public class JobSystem: MonoBehaviour
    {
        public event JobCreated JobCreated;
        public event JobRemoved JobRemoved;

        private IJobSimulator _jobSimulator = new JobSimulator.JobSimulator();
        public List<Item> AvailableItems;
        public float MinTimeToCompleteTaskSeconds = 30;
        public float MaxTimeToCompleteTaskSeconds = 60;
        public int MinTasks = 1;
        public int MaxTasks = 7;
        public float PeriodBetweenUpdates = 10f;
        private float timeRemainingBeforeUpdate = 0f;
        public IEnumerable<IMission> Missions => _jobSimulator.GetCurrentMissions().Values;

        void Start()
        {
            StartLevel();
        }

        public void Update()
        {
            timeRemainingBeforeUpdate -= Time.deltaTime;

            if (timeRemainingBeforeUpdate <= 0f)
            {
                UpdateLevel();
                timeRemainingBeforeUpdate = PeriodBetweenUpdates;
            }

            _jobSimulator.UpdateMissions(Time.deltaTime);
        }

        public void UpdateLevel()
        {
            var missions = _jobSimulator.GetCurrentMissions();
            foreach (var keyValuePair in missions
                .Where(keyValuePair => keyValuePair.Value.TimeToComplete.HasValue && keyValuePair.Value.IsComplete == false)
                .Where(keyValuePair => keyValuePair.Value.TimeToComplete.HasValue && keyValuePair.Value.TimeToComplete < 0))
            {
                _jobSimulator.RemoveMission(keyValuePair.Key);
            }

            if (_jobSimulator.GetCurrentMissions().Count < Random.Range(MinTasks, MaxTasks))
            {
                var item = GetRandomItem();
                var job = _jobSimulator.AddNewMission(item, Random.Range(MinTimeToCompleteTaskSeconds, MaxTimeToCompleteTaskSeconds));
                JobCreated?.Invoke(job);
            }
        }

        private Item GetRandomItem()
        {
            var item = AvailableItems[Random.Range(0, AvailableItems.Count)];

            item.SubCategory = (ItemSubCategory)UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(ItemSubCategory)).Length - 1);

            return item;
        }

        public void StartLevel()
        {
            _jobSimulator = new JobSimulator.JobSimulator();
            // _jobSimulator.AddNewMission(new MopStain(), 0);
        }

        private void OnJobRemoved(System.Guid jobId)
        {
            JobRemoved?.Invoke(jobId);
        }
    }
}