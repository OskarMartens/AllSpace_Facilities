using System.Reflection;

namespace AllSpace_Facilities.Entities
{
    public abstract class Vehicle : IVehicle
    {
        public string LicensePlate { get; private set; }
        public int NumOfWheels { get; private set; }
        public string Color { get; private set; }

        protected Vehicle(string licensePlate, int numOfWheels, string color)
        {
            LicensePlate = licensePlate;
            NumOfWheels = numOfWheels;
            Color = color;
        }

        public virtual string Stats() =>
            $"License plate:\t\t{LicensePlate}\nNumber of wheels:\t{NumOfWheels}\nColor:\t\t\t{Color}";

        public static List<string> GetPropertyNames()
        {
            var propertyNames = new List<string>();
            foreach (PropertyInfo prop in typeof(Vehicle).GetProperties())
            {
                propertyNames.Add(prop.Name);
            }
            return propertyNames;
        }
    }
}
