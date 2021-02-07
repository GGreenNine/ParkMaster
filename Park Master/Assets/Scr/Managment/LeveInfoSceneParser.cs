using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Scr.Mechanics;
using Scr.Mechanics.Car;
using UnityEngine;

public class LeveInfoSceneParser : MonoBehaviour
{
    private LevelInfo _levelInfo;
    private readonly List<InGameBonusInfo> _inGameBonusList = new List<InGameBonusInfo>();
    private List<CarInfo> _cars;

    [SerializeField] private int _level;
    private string pathToSave;

    [ContextMenu("ParseScene")]
    public void ParseScene()
    {
        pathToSave = $"B:/WorkProjects/ParkMaster/ParkMasterTestProject/Park Master/Assets/Resources/Levels/Level_{_level}.txt";
        var cars = GameObject.FindObjectsOfType<CarController>();
        var bonuses = FindObjectsOfType<InGameTriggeredBonusBase>();

        foreach (var carController in cars)
        {
            if (carController != null)
            {
                _cars.Add(new CarInfo(carController.CarType, carController.transform.position,
                    carController.transform.rotation));
            }
        }

        foreach (var bonus in bonuses)
        {
            if (bonus != null)
            {
                _inGameBonusList.Add(new InGameBonusInfo(bonus.InGameBonusType, bonus.transform.position,
                    bonus.transform.rotation));
            }
        }

        _levelInfo = new LevelInfo(_level, _inGameBonusList, _cars);
        
        WriteToDisk();
    }

    private void WriteToDisk()
    {
        using (StreamWriter file = File.CreateText(pathToSave))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializer.Serialize(file, _levelInfo);
        }
    }
    
}