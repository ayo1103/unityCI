using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Builder
{
    [MenuItem("BuildTool/Build")]
    public static void Build()
    {
        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new[] {"Assets/Scenes/SampleScene.unity"},
            locationPathName = "Builds/Win64/Game.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None,
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}