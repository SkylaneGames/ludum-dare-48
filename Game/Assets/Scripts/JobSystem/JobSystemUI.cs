using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MissionSystem.Mission;
using UnityEngine;

namespace MissionSystem.JobSystem
{
    [RequireComponent(typeof(JobSystem))]
    public class JobSystemUI : MonoBehaviour
    {
        public GameObject JobPrefab;
        public Transform JobPanel;

        private JobSystem _jobSystem;

        void Awake()
        {
            _jobSystem = GetComponent<JobSystem>();
            _jobSystem.JobCreated += RegisterJob;
        }

        void RegisterJob(IMission job)
        {
            var newObject = Instantiate(JobPrefab, JobPanel);

            var missionUi = newObject.GetComponent<MissionUi>();

            missionUi.Init(job);
        }
    }
}
