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
        GenerateVersionJson();
        
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