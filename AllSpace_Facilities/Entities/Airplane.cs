using System.Reflection;

namespace AllSpace_Facilities.Entities
{
    internal class Airplane : Vehicle, IAirplane
    {
        public int NumberOfEngines { get; private set; }

        public Airplane(int numberOfEngines, string licensePlate, int numOfWheels, string color) : base(licensePlate, numOfWheels, color)
        {
            NumberOfEngines = numberOfEngines;
        }

        public override string Stats() =>
            $"Vehicle type:\t\t{typeof(Airplane).Name}\nNumberOfEngines:\t{NumberOfEngines}\n" + base.Stats();

        public static List<string> GetAirplanePropertyNames()
        {
            var propertyNames = new List<string>();
            foreach (PropertyInfo prop in typeof(Airplane).GetProperties())
            {
                propertyNames.Add(prop.Name);
            }
            return propertyNames;
        }
    }
}
