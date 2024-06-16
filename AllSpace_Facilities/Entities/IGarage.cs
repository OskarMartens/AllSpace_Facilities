namespace AllSpace_Facilities.Entities
{

    internal interface IGarage : IEnumerable<Vehicle>
    {
        int Id { get; }
        int ParkingCapacity { get; }
    }

    internal interface IGarage<T> : IGarage where T : Vehicle
    {
        void AddVehicle(T item);
    }
}
