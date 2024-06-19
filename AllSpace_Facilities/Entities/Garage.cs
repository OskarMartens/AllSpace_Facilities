using AllSpace_Facilities.UI;
using System.Collections;

namespace AllSpace_Facilities.Entities
{
    public class Garage<T> : IGarage<T> where T : Vehicle
    {
        public int ParkingCapacity { get; private set; }

        public T[] Vehicles { get; private set; }

        public IUI Ui = new ConsoleUI();

        public Garage(int parkingCapacity)
        {
            ParkingCapacity = parkingCapacity;
            Vehicles = new T[ParkingCapacity];
        }

        public void AddVehicle(T vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            if (Vehicles.Any(v => v != null && v.LicensePlate.Equals(vehicle.LicensePlate)))
                throw new InvalidOperationException("There is already a vehicle with that licence plate");
            for (int i = 0; i < Vehicles.Length; i++)
            {
                if (Vehicles[i] == null)
                {
                    Vehicles[i] = vehicle;
                    Ui.PrintLine($"You successfully parked a {vehicle.GetType().Name} with the license plate: {vehicle.LicensePlate}");
                    return;
                }
            }
            throw new InvalidOperationException("Garage is full.");
        }

        public void GetGarageInfo()
        {
            if (!Vehicles.Any(v => v != null))
            {
                Ui.PrintLine($"There are no {typeof(T).Name.ToLower()}s in the garage");
                return;
            }

            var vehicleGroups = Vehicles.Where(v => v != null)
                                        .GroupBy(v => v.GetType().Name)
                                        .Select(group => new { Type = group.Key, Count = group.Count() });

            Ui.PrintLine("There are:");
            foreach (var group in vehicleGroups)
            {
                Ui.PrintLine($"There are {group.Count} {group.Type.ToLower()}s in the garage");
            }
        }


        public void GetByLicensePlate(string licensePlate)
        {
            var vehicle = Vehicles.FirstOrDefault(v => v != null && v.LicensePlate.ToLower() == licensePlate.ToLower());
            if (vehicle != null)
            {
                if (vehicle is Car car)
                {
                    Ui.PrintLine(car.Stats());
                    Ui.PrintLine("");

                }
                else if (vehicle is Bus bus)
                {
                    Ui.PrintLine(bus.Stats());
                    Ui.PrintLine("");
                }
                else
                    Ui.PrintLine(vehicle.Stats());
            }
            else
            {
                Ui.PrintLine($"No vehicles with license plate: {licensePlate} found.");
            }
        }


        public void RemoveVehicle(string licensePlate)
        {
            for (int i = 0; i < Vehicles.Length; i++)
            {
                if (Vehicles[i] != null && Vehicles[i]!.LicensePlate == licensePlate)
                {
                    Vehicles[i] = null!;
                    Ui.PrintLine($"The vehicle with with the license plate: {licensePlate} was successfully removed.");
                    return;
                }
            }
            Ui.PrintLine($"No vehicle with the license plate: {licensePlate} was found");
        }

        public IEnumerator<T> GetEnumerator()
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

        public string GetVehicleType()
        {
            return typeof(T).Name;
        }
    }
}
