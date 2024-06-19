using System.Reflection;

namespace AllSpace_Facilities.Entities
{
    public class Car : Vehicle
    {
        public string FuelType { get; private set; }

        public Car(string fuelType, string licensePlate, string color)
            : base(licensePlate, 4, color)
        {
            FuelType = fuelType;
        }

        public override string Stats() =>
            $"Vehicle type:\t\t{typeof(Car).Name}\nFuelType:\t\t{FuelType}\n" + base.Stats();

        public static List<string> GetCarPropertyNames()
        {
            var propertyNames = new List<string>();
            foreach (PropertyInfo prop in typeof(Car).GetProperties())
            {
                propertyNames.Add(prop.Name);
            }
            return propertyNames;
        }
    }
}
