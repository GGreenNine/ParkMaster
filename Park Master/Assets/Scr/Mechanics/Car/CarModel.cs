using Scr.Mechanics.Bezier;
using UnityEngine;

namespace Scr.Mechanics.Car
{
    public class CarModel
    {
        public readonly CarType CarType;
        public readonly float CarSpeed;
        public readonly Vector3 InitialPos;
        public readonly Quaternion InitialRotation;

        public CarModel(CarType carType, float carSpeed, Quaternion initialRotation, Vector3 initialPos)
        {
            CarType = carType;
            CarSpeed = carSpeed;
            InitialRotation = initialRotation;
            InitialPos = initialPos;
        }
    }
}