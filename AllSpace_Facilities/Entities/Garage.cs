using System.Collections;

namespace AllSpace_Facilities.Entities
{
    internal class Garage<T> : IGarage<T> where T : Vehicle
    {
        public int Id { get; private set; }
        public int ParkingCapacity { get; private set; }
        public T[] Vehicles { get; set; }

        public Garage(int id, int parkingCapacity)
        {
            Id = id;
            ParkingCapacity = parkingCapacity;
            Vehicles = new T[ParkingCapacity];
        }

        public void AddVehicle(T vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            for (int i = 0; i < Vehicles.Length; i++)
            {
                if (Vehicles[i] == null)
                {
                    Vehicles[i] = vehicle;
                    return;
                }
            }
            throw new InvalidOperationException("Garage is full.");
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            foreach (var vehicle in Vehicles)
            {
                if (vehicle != null)
                {
                    yield return vehicle;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
