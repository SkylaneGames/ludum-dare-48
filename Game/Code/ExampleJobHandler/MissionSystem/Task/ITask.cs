namespace MissionSystem.Task
{
    public interface ITask
    {
        public string TaskName { get; }
        public string AnimationIndex { get; }
        public string TaskImage { get; }
    }
}