namespace MissionSystem.Task
{
    public class MopStain: ITask
    {
        public string TaskName { get; } = "Mop Stain";
        public string AnimationIndex { get; } = "mopping";
        public string TaskImage { get; } = "mop.png";
    }
}