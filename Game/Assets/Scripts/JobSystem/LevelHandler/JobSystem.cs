using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoreSystems;
using MissionSystem.JobSimulator;
using MissionSystem.Mission;
using MissionSystem.Task;
using UnityEngine;

namespace MissionSystem.JobSystem
{
    public delegate void JobCreated(IMission job);
    public delegate void JobRemoved(System.Guid jobId);

    public class JobSystem : Singleton<JobSystem>
    {
        public event JobCreated JobCreated;

        private IJobSimulator _jobSimulator;
        public List<Item> AvailableItems;

        [Range(5, 60)]
        public float MinTimeToCompleteTaskSeconds = 30;

        [Range(10, 60)]
        public float MaxTimeToCompleteTaskSeconds = 60;

        [Range(0, 10)]
        public int MinTasks = 1;

        [Range(0, 10)]
        public int MaxTasks = 7;

        [Range(0, 15f)]
        public float MinPeriodBetweenUpdates = 3f;

        [Range(0, 20f)]
        public float MaxPeriodBetweenUpdates = 10f;
        
        private float timeRemainingBeforeUpdate = 0f;
        public IEnumerable<IMission> Missions => _jobSimulator.GetCurrentMissions().Values;

        void Start()
        {
            _jobSimulator = new JobSimulator.JobSimulator();
        }

        public void Update()
        {
            timeRemainingBeforeUpdate -= Time.deltaTime;

            if (timeRemainingBeforeUpdate <= 0f)
            {
                UpdateLevel();
                timeRemainingBeforeUpdate = Random.Range(MinPeriodBetweenUpdates, MaxPeriodBetweenUpdates);
            }

            _jobSimulator.UpdateMissions(Time.deltaTime);
        }

        public void UpdateLevel()
        {
            var missions = _jobSimulator.GetCurrentMissions();

            if (missions.Count < Random.Range(MinTasks, MaxTasks))
            {
                var item = GetRandomItem();
                var job = _jobSimulator.AddNewMission(item, Random.Range(MinTimeToCompleteTaskSeconds, MaxTimeToCompleteTaskSeconds));
                JobCreated?.Invoke(job);
            }
        }

        private Item GetRandomItem()
        {
            var item = AvailableItems[Random.Range(0, AvailableItems.Count)].CreateInstance();

            item.SubCategory = (ItemSubCategory)UnityEngine.Random.Range(0, System.Enum.GetNames(typeof(ItemSubCategory)).Length);

            return item;
        }

        public bool JobExistsFor(Item item)
        {
            return _jobSimulator.GetCurrentMissions().Any(p => p.Value.Item == item);
        }

        public bool CompleteJob(Item item)
        {
            var jobId = _jobSimulator.GetCurrentMissions()
                .Where(p => p.Value.Item == item)
                .OrderBy(p => p.Value.TimeRemaining)
                .Select(p => p.Key)
                .FirstOrDefault();

            if (jobId == System.Guid.Empty)
            {
                return false;
            }

            _jobSimulator.CompleteMission(jobId);

            return true;
        }

        public IEnumerable<Item> GetRequiredItems()
        {
            var items = _jobSimulator.GetCurrentMissions()
                .OrderBy(p => p.Value.TimeRemaining)
                .Select(p => p.Value.Item);

            return items;
        }
    }
}