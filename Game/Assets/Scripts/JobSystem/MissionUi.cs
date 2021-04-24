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
        public Image Background;

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
            Image.sprite = mission.Item.Sprite;
            SetSubCategory(mission.Item.SubCategory);
        }

        public void SetSubCategory(ItemSubCategory category)
        {
            switch (category)
            {
                case ItemSubCategory.Red:
                    Background.color = new Color(80 / 100f, 36 / 100f, 16 / 100f);
                    break;
                case ItemSubCategory.Orange:
                    Background.color = new Color(90 / 100f, 67 / 100f, 29 / 100f);
                    break;
                case ItemSubCategory.Green:
                    Background.color = new Color(45 / 100f, 77 / 100f, 58 / 100f);
                    break;
                case ItemSubCategory.Blue:
                    Background.color = new Color(21 / 100f, 61 / 100f, 74 / 100f);
                    break;
                case ItemSubCategory.Purple:
                    Background.color = new Color(69 / 100f, 45 / 100f, 69 / 100f);
                    break;
            }
        }
    }
}
