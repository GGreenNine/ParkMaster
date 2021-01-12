using System;
using System.Linq;
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
    
    [CreateAssetMenu(fileName = "PrefabPathConfig", menuName = "ResoursesPath")]
    public class PrefabPathConfig : ScriptableObject
    {
        public string lineRendererPathBuilder;
        public CarPrefabPathes[] carPrefabPatheses;
        public string blueCarPath;


        public string GetCarPathByCarType(CarType type)
        {
            return carPrefabPatheses.First(pathes => pathes._type == type).carPath;
        }
    }
}