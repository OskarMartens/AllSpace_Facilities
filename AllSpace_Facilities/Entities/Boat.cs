using System.Reflection;

namespace AllSpace_Facilities.Entities
{
    internal class Boat : Vehicle, IBoat
    {
        public Boat(int length, string licensePlate, string color)
            : base(licensePlate, 0, color)
        {
            Length = length;
        }

        public int Length { get; private set; }
        public override string Stats() =>
            $"Vehicle type:\t\t{typeof(Boat).Name}\nLength:\t\t{Length}\n" + base.Stats();

        public static List<string> GetBoatPropertyNames()
        {
            var propertyNames = new List<string>();
            foreach (PropertyInfo prop in typeof(Boat).GetProperties())
            {
                propertyNames.Add(prop.Name);
            }
            return propertyNames;
        }
    }
}
