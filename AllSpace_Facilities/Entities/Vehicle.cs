namespace AllSpace_Facilities.Entities
{
    internal abstract class Vehicle
    {
        public string LicensePlate { get; set; }
        public int numOfWheels { get; set; }

        protected Vehicle(string licensePlate, int numOfWheels)
        {
            LicensePlate = licensePlate;
            this.numOfWheels = numOfWheels;
        }
    }
}
