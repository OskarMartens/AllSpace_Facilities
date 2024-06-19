namespace AllSpace_Facilities.Entities
{
    internal interface IBus : IVehicle
    {
        int NumOfSeats { get; }

        string Stats();
    }
}