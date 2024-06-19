namespace AllSpace_Facilities.Entities
{
    internal interface IBoat : IVehicle
    {
        int Length { get; }

        string Stats();
    }
}