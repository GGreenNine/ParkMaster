using Scr.Mechanics.Bezier;

namespace Scr.Mechanics.Car
{
    public class CarModel
    {
        public readonly CarType CarType;
        public readonly float CarSpeed;

        public CarModel(CarType carType, float carSpeed)
        {
            CarType = carType;
            CarSpeed = carSpeed;
        }
    }
}