namespace AllSpace_Facilities.Entities
{
    internal interface ICar : IVehicle
    {
        string FuelType { get; }

        string Stats();
    }
}