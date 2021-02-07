using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public abstract class JsonDataLoader<T>
{
    public abstract T Load();
}

public class LevelLoadParser : JsonDataLoader<List<LevelInfo>>
{
    private const string directoryPath = "B:/WorkProjects/ParkMaster/ParkMasterTestProject/Park Master/Assets/Resources/Levels/";
    
    public override List<LevelInfo> Load()
    {
        if (Directory.Exists(directoryPath))
        {
            return ProcessDirectory(directoryPath);
        }

        throw new Exception("Directory doesn't exist");
    }
    
    public List<LevelInfo> ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        List<LevelInfo> levelInfos = new List<LevelInfo>();
        var files = new DirectoryInfo(targetDirectory).GetFiles().Where(info => info.Extension != ".meta").ToList();
        foreach (var file in files)
        {
            levelInfos.Add(ProcessLevelFile(file));
        }

        return levelInfos;
    }

    // Insert logic for processing found files here.
    public static LevelInfo ProcessLevelFile(FileInfo path)
    {
        var fileText = File.ReadAllText(path.FullName);
        if (!string.IsNullOrWhiteSpace(fileText))
        {
            var levelInfo = JsonConvert.DeserializeObject<LevelInfo>(fileText);
            return levelInfo;
        }

        throw new Exception("Path is not correct");
    }
}
