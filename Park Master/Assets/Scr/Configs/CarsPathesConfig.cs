using System;
using System.Linq;
using ModestTree;
using Scr.Mechanics.Bezier;
using UnityEngine;

namespace Scr.Configs
{
    [CreateAssetMenu(fileName = "BezierSettings", menuName = "GameSettings")]
    public class CarsPathesConfig : ScriptableObject
    {
        [Serializable]
        public class LineRendererSettings
        {
            public Color color;
            public float width;
        }
        
        [Serializable]
        public class LineRendererByCarSettings
        {
            public CarType CarType;
            public LineRendererSettings LineRendererSettings;
        }

        [SerializeField]
        private LineRendererByCarSettings[] lineRendererByCarSettings;

        public LineRendererByCarSettings GetLineRendererSettings(CarType type)
        {
            var settings = lineRendererByCarSettings.FirstOrDefault(carSettings => carSettings.CarType == type);
            Assert.IsNotNull(settings, $"There is no line renderer setting with car type : {type.ToString()}");
            return settings;
        }
    }
}