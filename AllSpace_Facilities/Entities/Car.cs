namespace AllSpace_Facilities.Entities
{
    internal class Car : Vehicle
    {
        public string FuelType { get; set; }

        public Car(string fuelType, string licensePlate, int numOfWheels)
            : base(licensePlate, numOfWheels)
        {
            FuelType = fuelType;
        }
    }
}
