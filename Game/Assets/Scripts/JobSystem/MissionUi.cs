using System.Collections;
using System.Collections.Generic;
using MissionSystem.Mission;
using UnityEngine;
using UnityEngine.UI;

namespace MissionSystem.JobSystem
{
    public class MissionUi : MonoBehaviour
    {
        public IMission Mission { get; private set; }
        private Slider _slider;
        public Image Image;

        void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Mission != null)
            {
                _slider.normalizedValue = Mission.NormalisedTimeRemaing ?? 0f;

                if ((Mission.TimeRemaining ?? 1f) <= 0f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        public void Init(IMission mission)
        {
            Mission = mission;
            Image.sprite = mission.Task.TaskImage;
        }
    }
}
