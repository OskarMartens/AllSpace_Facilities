using System.Reflection;

namespace AllSpace_Facilities.Entities
{
    internal class Motorcycle : Vehicle, IMotorcycle
    {
        public int CylinderVolume { get; private set; }

        public Motorcycle(int cylinderVolume, string licensePlate, int numOfWheels, string color)
            : base(licensePlate, numOfWheels, color)
        {
            CylinderVolume = cylinderVolume;
        }
        public override string Stats() =>
                    $"Vehicle type:\t\t{typeof(Motorcycle).Name}\nCylinder volume:\t{CylinderVolume}\n" + base.Stats();

        public static List<string> GetMotorcyclePropertyNames()
        {
            var propertyNames = new List<string>();
            foreach (PropertyInfo prop in typeof(Motorcycle).GetProperties())
            {
                propertyNames.Add(prop.Name);
            }
            return propertyNames;
        }
    }
}
