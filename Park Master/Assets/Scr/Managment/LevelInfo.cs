using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Scr.Mechanics;
using Scr.Mechanics.Bezier;
using UnityEngine;

[Serializable]
public class InGameBonusInfo
{
    [JsonConverter(typeof(StringEnumConverter))]
    public readonly InGameBonusType InGameBonusType;
    public readonly Vector3 position;
    public readonly Quaternion rotation;
    public readonly CarType canCollectedBy;

    public InGameBonusInfo(InGameBonusType inGameBonusType, Vector3 position, Quaternion rotation, CarType canCollectedBy)
    {
        InGameBonusType = inGameBonusType;
        this.position = position;
        this.rotation = rotation;
        this.canCollectedBy = canCollectedBy;
    }
}

[Serializable]
public class CarInfo
{
    [JsonConverter(typeof(StringEnumConverter))]
    public readonly CarType CarType;
    public readonly Vector3 position;
    public readonly Quaternion rotation;

    public CarInfo(CarType carType, Vector3 position, Quaternion rotation)
    {
        CarType = carType;
        this.position = position;
        this.rotation = rotation;
    }
}

[Serializable]
public class LevelInfo
{
    public readonly int level;
    public readonly List<InGameBonusInfo> InGameBonusList;
    public readonly List<CarInfo> Cars;

    public LevelInfo(int level, List<InGameBonusInfo> inGameBonusList, List<CarInfo> cars)
    {
        this.level = level;
        InGameBonusList = inGameBonusList;
        Cars = cars;
    }

    public int GetBonusesCount(InGameBonusType bonusType)
    {
        var bonusCount = InGameBonusList.Count(info => info.InGameBonusType == bonusType);
        return bonusCount;
    }
}
