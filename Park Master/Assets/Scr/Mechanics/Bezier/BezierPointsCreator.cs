using System.Collections.Generic;
using Scr.Configs;
using UnityEngine;

namespace Scr.Mechanics.Bezier
{
    public interface IPointsCreator
    {
        List<Vector3> GetPoints(Vector3[] points);
    }
    
    public enum CarType
    {
        None = 0,
        Blue = 1,
        Yellow = 2
    }

    public class BezierPointsCreator : IPointsCreator
    {
        private readonly BezierSettingsConfig _bezierSettingsConfig;
    
        public BezierPointsCreator(BezierSettingsConfig bezierSettingsConfig)
        {
            _bezierSettingsConfig = bezierSettingsConfig;
        }

        public List<Vector3> GetPoints(Vector3[] points)
        {
            var precission = _bezierSettingsConfig.bezierPrecision;
            var result = new List<Vector3>();
            for (var i = 0; i < points.Length; i++)
            {
                if ((i + 1) % 3 != 0) continue;
                var p0 = points[i - 2];
                var p1 = points[i - 1];
                var p2 = points[i];

                for (int j = 0; j < precission; j++)
                {
                    float t = j / (float) precission;
                    var newBezierPoint = CalculateQuadraticBezierPoint(t, p0, p1, p2);
                    result.Add(newBezierPoint);
                }
            }

            return result;
        }

        private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            var u = 1 - t;
            var tt = t * t;
            var uu = u * u;
            var p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            return p;
        }
    }
}
