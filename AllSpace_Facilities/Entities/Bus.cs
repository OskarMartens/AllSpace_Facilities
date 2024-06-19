
using System.Reflection;

namespace AllSpace_Facilities.Entities
{
    public class Bus : Vehicle, IBus
    {
        public int NumOfSeats { get; private set; }

        public Bus(int numOfSeats, string licensePlate, int numOfWheels, string color)
            : base(licensePlate, numOfWheels, color)
        {
            NumOfSeats = numOfSeats;
        }

        public override string Stats() =>
            $"Vehicle type:\t\t{typeof(Bus).Name}\nNumber of seats:\t{NumOfSeats}\n" + base.Stats();

        public static List<string> GetBusPropertyNames()
        {
            var propertyNames = new List<string>();
            foreach (PropertyInfo prop in typeof(Bus).GetProperties())
            {
                propertyNames.Add(prop.Name);
            }
            return propertyNames;
        }
    }
}
