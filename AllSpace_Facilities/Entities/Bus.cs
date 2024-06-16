namespace AllSpace_Facilities.Entities
{
    internal class Bus : Vehicle
    {
        public int numOfSeats { get; set; }

        public Bus(int numOfSeats, string licensePlate, int numOfWheels)
            : base(licensePlate, numOfWheels)
        {
            this.numOfSeats = numOfSeats;
        }
    }
}
