namespace AllSpace_Facilities.Entities
{
    internal interface IMotorcycle : IVehicle
    {
        int CylinderVolume { get; }

        string Stats();
    }
}