using System;
using System.Linq;
using Scr.Mechanics;
using Scr.Mechanics.Bezier;
using UnityEngine;

namespace Scr.Configs
{
    [Serializable]
    public struct CarPrefabPathes
    {
        public CarType _type;
        public string carPath;
    }
    
    [Serializable]
    public struct BonusesPrefabPathes
    {
        public InGameBonusType _bonusType;
        public string path;
    }
    
    [CreateAssetMenu(fileName = "PrefabPathConfig", menuName = "ResoursesPath")]
    public class PrefabPathConfig : ScriptableObject
    {
        public string lineRendererPathBuilder;
        public BonusesPrefabPathes[] bonusesPrefabPathes;
        public CarPrefabPathes[] carPrefabPatheses;
        
        public string GetCarPathByCarType(CarType type)
        {
            return carPrefabPatheses.First(pathes => pathes._type == type).carPath;
        }
        public string GetBonusPathByBonusType(InGameBonusType bonusType)
        {
            return bonusesPrefabPathes.First(pathes => pathes._bonusType == bonusType).path;
        }
        
    }
}