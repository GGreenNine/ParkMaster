using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scr.Mechanics;
using Scr.Mechanics.Bezier;
using UnityEngine;
using System.IO;

public abstract class JsonDataLoader<T>
{
    public abstract T Load();
}

public class LevelLoadParser : JsonDataLoader<List<LevelInfo>>
{
    public override List<LevelInfo> Load()
    {
        var inGameBonuses = new List<InGameBonusInfo>()
        {
            new InGameBonusInfo(InGameBonusType.Coin, Vector3.one, Quaternion.identity, CarType.Blue),
            new InGameBonusInfo(InGameBonusType.Coin, Vector3.one, Quaternion.identity, CarType.Yellow),
            new InGameBonusInfo(InGameBonusType.Key, Vector3.one, Quaternion.identity, CarType.Any)
        };
        var cars = new List<CarInfo>()
        {
            new CarInfo(CarType.Blue, Vector3.one, Quaternion.identity)
        };
        var levelInfoTest = new LevelInfo(1, inGameBonuses, cars);
        var json = JsonConvert.SerializeObject(levelInfoTest, Formatting.None, new JsonSerializerSettings 
        { 
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        var path = $"{Application.dataPath}/Resources/Levels";
        
        if (Directory.Exists(path))
        {
            return ProcessDirectory(path);
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
    private LevelInfo ProcessLevelFile(FileSystemInfo path)
    {
        var fileText = File.ReadAllText(path.FullName);
        if (string.IsNullOrWhiteSpace(fileText))
        {
            throw new Exception("Path is not correct");
        }
        var levelInfo = JsonConvert.DeserializeObject<LevelInfo>(fileText);
        return levelInfo;

    }
}
