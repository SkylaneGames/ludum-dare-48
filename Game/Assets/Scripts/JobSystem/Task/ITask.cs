using UnityEngine;

namespace MissionSystem.Task
{
    public interface ITask
    {
        public string TaskName { get; }
        public string AnimationIndex { get; }
        public Sprite TaskImage { get; }
    }
}