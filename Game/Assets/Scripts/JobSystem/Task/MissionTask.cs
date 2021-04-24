using UnityEngine;

namespace MissionSystem.Task
{
    [CreateAssetMenu(fileName = "Task", menuName = "Jobs/Task")]
    public class MissionTask : ScriptableObject, ITask
    {
        [SerializeField]
        private string Name;
        public string TaskName => Name;

        [SerializeField]
        public string AnimationTrigger;
        public string AnimationIndex => AnimationTrigger;
        
        [SerializeField]
        public Sprite Image;
        public Sprite TaskImage => Image;
    }
}