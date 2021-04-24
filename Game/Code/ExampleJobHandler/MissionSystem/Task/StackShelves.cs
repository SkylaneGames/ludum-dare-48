namespace MissionSystem.Task
{
    public class StackShelves: ITask
    {
        public string TaskName { get; } = "Stack Shelves";
        public string AnimationIndex { get; } = "stacking";
        public string TaskImage { get; } = "stacking.png";
    }
}