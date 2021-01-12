using System;
using UnityEngine;

namespace Scr.Extensions
{
    public static class UnityExtensions{
 
        /// <summary>
        /// Extension method to check if a layer is in a layermask
        /// </summary>
        /// <returns></returns>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        public static bool AlmostEqual(this Vector3 targetPosition, Vector3 currentPosition, float precission)
        {
            return Math.Abs(targetPosition.x - currentPosition.x) < precission 
                   && Math.Abs(targetPosition.y - currentPosition.y) < precission 
                   && Math.Abs(targetPosition.z - currentPosition.z) < precission;
        }
    }
}