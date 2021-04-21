using UnityEditor;

class BuildHelper
{
    static string[] scenes = { "Assets/Scenes/Splash.unity","Assets/Scenes/Main Menu.unity","Assets/Scenes/Game.unity" };
    
    static void PerformBuildWebGL()
    {
        Build(BuildTarget.WebGL);
    }
    
    static void PerformBuildWin64()
    {
        Build(BuildTarget.StandaloneWindows64);
    }
    
    static void PerformBuildOSX()
    {
        Build(BuildTarget.StandaloneOSX);
    }
    
    static void PerformBuildLinux()
    {
        Build(BuildTarget.StandaloneLinux64);
    }

    static void Build(BuildTarget target)
    {
        BuildPipeline.BuildPlayer(scenes, "../build", target, BuildOptions.None);
    }
}