namespace AllSpace_Facilities.Entities
{
    public interface IVehicle
    {
        string Color { get; }
        string LicensePlate { get; }
        int NumOfWheels { get; }
        string Stats();
    }
}