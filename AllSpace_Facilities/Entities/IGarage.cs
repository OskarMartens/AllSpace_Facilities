namespace AllSpace_Facilities.Entities
{

    public interface IGarage<T> : IEnumerable<T> where T : Vehicle
    {
        int ParkingCapacity { get; }
        void AddVehicle(T item);
        void RemoveVehicle(string licensePlate);
        string GetVehicleType();
        void GetGarageInfo();
        void GetByLicensePlate(string licensePlate);
    }
}
