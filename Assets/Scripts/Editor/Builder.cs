using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Builder
{
    private const string BuildPath_Win64_BuildVersion = "/Builds/Win64/StealthDuskGame.json";

    [Serializable]
    public class BuildVersion
    {
        public string version;
    }

    [MenuItem("BuildTool/GenerateVersionJson")]
    public static void GenerateVersionJson()
    {
        var buildVersion = new BuildVersion
        {
            version = Application.version
        };

        string jsonPath = Directory.GetCurrentDirectory() + BuildPath_Win64_BuildVersion;
        string contents = JsonUtility.ToJson(buildVersion);
        File.WriteAllText(jsonPath, contents);
    }

    [MenuItem("BuildTool/Build")]
    public static void Build()
    {
        var buildPlayerOptions = GetBuildPlayerOptions();
        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        var summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            GenerateVersionJson();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    private static BuildPlayerOptions GetBuildPlayerOptions()
    {
        var buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new[] {"Assets/Scenes/SampleScene.unity"},

#if UNITY_EDITOR_WIN
            locationPathName = "Builds/Win64/Game.exe",
            target = BuildTarget.StandaloneWindows64,
            
#elif UNITY_EDITOR_OSX
            locationPathName = "Builds/macOS/"

#endif
            options = BuildOptions.None,
        };
        
        return buildPlayerOptions;
    }
}