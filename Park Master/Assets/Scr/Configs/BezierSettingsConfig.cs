using UnityEngine;

namespace Scr.Configs
{
    [CreateAssetMenu(fileName = "BezierSettings", menuName = "GameSettings")]
    public class BezierSettingsConfig : ScriptableObject
    {
        public int bezierPrecision = 50;
    }
}
