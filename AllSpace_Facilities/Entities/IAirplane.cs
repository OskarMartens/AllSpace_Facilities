namespace AllSpace_Facilities.Entities
{
    internal interface IAirplane : IVehicle
    {
        int NumberOfEngines { get; }

        string Stats();
    }
}