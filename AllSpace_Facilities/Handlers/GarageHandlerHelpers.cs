using AllSpace_Facilities.Entities;
using AllSpace_Facilities.Enums;
using AllSpace_Facilities.UI;

namespace AllSpace_Facilities.Handlers
{
    public class GarageHandlerHelpers : IGarageHandlerHelpers
    {
        public IUI Ui { get; }
        public IEnumerable<VehicleTypes> VehicleTypes { get; }
        public IGarage<Vehicle> Garage { get; }
        public GarageHandlerHelpers(IUI ui, IEnumerable<VehicleTypes> vehicleTypes, IGarage<Vehicle> garage)
        {
            Ui = ui;
            VehicleTypes = vehicleTypes;
            Garage = garage;
        }

        public void GetAllVehicles()
        {
            NoVehicles();
            foreach (var item in Garage)
            {
                Ui.PrintLine(item.Stats());
                Ui.PrintLine("");
            };
        }

        public void NoVehicles()
        {
            if (!Garage.Any())
            {
                Ui.PrintLine($"There are no {typeof(IVehicle).Name.ToLower()}s in this garage.");
            }
        }

        public void PrintAvailableSpots()
        {
            int availableSpots = AvailableSpotsCount();
            Ui.PrintLine($"Available parking spots: {availableSpots}");
        }

        public bool IsGarageFull()
        {
            if (AvailableSpotsCount() > 0)
                return false;
            else
                return true;
        }

        public int AvailableSpotsCount()
        {
            int occupiedSpots = 0;
            foreach (var vehicle in Garage)
            {
                occupiedSpots++;
            }
            int availableSpots = Garage.ParkingCapacity - occupiedSpots;
            return availableSpots;
        }

        public void GetGarageInfo()
        {
            if (!Garage.Any())
                Ui.PrintLine($"There are no {typeof(Vehicle).Name.ToLower()}s in the garage");
            else
            {
                var vehicleGroups = Garage.GroupBy(v => v.GetType().Name)
                          .Select(group => new { Type = group.Key, Count = group.Count() });

                Ui.PrintLine("There are:");
                foreach (var group in vehicleGroups)
                {
                    Ui.PrintLine($"{group.Count} {group.Type.ToLower()}s in the garage");
                }
            }
        }

        public void GetByLicensePlate()
        {
            Ui.PrintLine("Please enter the license plate");
            string licensePlate = Ui.GetInput();
            Garage.GetByLicensePlate(licensePlate);
        }

        public string GetUniqueLicensePlate()
        {
            string licensePlate;
            bool isUnique;

            do
            {
                Console.WriteLine("Please enter a license plate:");
                licensePlate = Console.ReadLine() ?? "";
                isUnique = !Garage.Any(vehicle => vehicle.LicensePlate == licensePlate && licensePlate != "");

                if (!isUnique)
                {
                    Console.WriteLine("This license plate already exists. Please enter a unique license plate.");
                }
            }
            while (!isUnique);

            return licensePlate!;
        }

        public void GetByProperties()
        {

            bool stillSelecting = true;
            do
            {
                Ui.PrintLine("What vehicle type would you like to look for?");
                int listNum = 1;
                foreach (var item in VehicleTypes.ToList())
                {
                    Ui.PrintLine($"{listNum}. {item}");
                    listNum++;
                }
                int option = Ui.GetValidInput(VehicleTypes.Count());
                switch (option)
                {
                    case 1:
                        stillSelecting = GetVehiclesBy();
                        break;
                    case 2:
                        stillSelecting = GetCarsBy();
                        break;
                    case 3:
                        stillSelecting = GetBussesBy();
                        break;
                    case 4:
                        stillSelecting = GetMotorcyclesBy();
                        break;
                    case 5:
                        stillSelecting = GetBoatsBy();
                        break;
                    case 6:
                        stillSelecting = GetAirplanesBy();
                        break;
                    default:
                        Ui.PrintLine("The input is not valid");
                        break;
                }
            } while (stillSelecting);
        }


        private void PrintResults(List<Vehicle> queryResult)
        {
            if (queryResult.Count == 0)
                Ui.PrintLine("There are no vehicles that matches does critera.");
            else
            {
                foreach (var v in queryResult)
                {
                    PrintVehicleStats(v);
                }
            }
        }

        private void PrintVehicleStats(Vehicle v)
        {
            if (v is ICar car)
            {
                Ui.PrintLine(car.Stats());
                Ui.PrintLine("");

            }
            else if (v is IBus bus)
            {
                Ui.PrintLine(bus.Stats());
                Ui.PrintLine("");
            }
            else if (v is IMotorcycle mc)
            {
                Ui.PrintLine(mc.Stats());
                Ui.PrintLine("");
            }
            else if (v is IBoat boat)
            {
                Ui.PrintLine(boat.Stats());
                Ui.PrintLine("");
            }
            else
                Ui.PrintLine(v.Stats());
        }
        private bool GetAirplanesBy()
        {
            List<string> airplaneProps = Airplane.GetAirplanePropertyNames();
            airplaneProps.RemoveAt(1);
            List<Vehicle> queryResult = [];
            bool stillSelecting = true;
            bool hasGarageBeenQueried = false;

            do
            {
                Ui.PrintLine("Which property would you like to find by?");
                int count = 1;
                foreach (var item in airplaneProps)
                {
                    Ui.PrintLine($"{count}. {item}");
                    count++;
                }
                Ui.PrintLine($"{count}. No specific property.");
                int option = Ui.GetValidInput(count);
                Ui.PrintLine("What would you like that property to be?");

                switch (option)
                {
                    case 1:
                        //Num of engines
                        int engineCount = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IAirplane airplane
                            && airplane.NumberOfEngines == engineCount).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IAirplane airplane
                            && airplane.NumberOfEngines == engineCount).ToList();
                        }
                        break;
                    case 2:
                        //Wheels
                        int numWheels = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IAirplane airplane
                            && airplane.NumOfWheels == numWheels).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IAirplane airplane
                            && airplane.NumOfWheels == numWheels).ToList();
                        }
                        break;
                    case 3:
                        //Color
                        string color = Ui.GetInput();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IAirplane airplane
                            && airplane.Color == color).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IAirplane airplane
                            && airplane.Color == color).ToList();
                        }
                        break;
                }
                Ui.PrintLine("Would you like to add another property to filter on?");
                string userInput = Ui.GetValidInput("y", "n");
                if (userInput.ToLower().Equals("n"))
                {
                    stillSelecting = false;
                }

            } while (stillSelecting);


            PrintResults(queryResult);
            return stillSelecting;
        }
        private bool GetBoatsBy()
        {
            List<string> boatProps = Boat.GetBoatPropertyNames();
            boatProps.RemoveAt(1);
            List<Vehicle> queryResult = [];
            bool stillSelecting = true;
            bool hasGarageBeenQueried = false;

            do
            {
                Ui.PrintLine("Which property would you like to find by?");
                int count = 1;
                foreach (var item in boatProps)
                {
                    Ui.PrintLine($"{count}. {item}");
                    count++;
                }
                Ui.PrintLine($"{count}. No specific property.");
                int option = Ui.GetValidInput(count);
                Ui.PrintLine("What would you like that property to be?");

                switch (option)
                {
                    case 1:
                        //Length
                        int length = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IBoat boat
                            && boat.Length == length).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IBoat boat
                            && boat.Length == length).ToList();
                        }
                        break;
                    case 2:
                        //NumOfWheels
                        int numWheels = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IBoat boat
                            && boat.NumOfWheels == numWheels).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IBoat boat
                            && boat.NumOfWheels == numWheels).ToList();
                        }
                        break;
                    case 3:
                        //Color
                        string color = Ui.GetInput();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IBoat boat
                            && boat.Color == color).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IBoat boat
                            && boat.Color == color).ToList();
                        }
                        break;
                    case 4:
                        //No specific property
                        queryResult = Garage.Where(v => v != null && v is Boat boat).ToList();
                        break;
                }
                Ui.PrintLine("Would you like to add another property to filter on?");
                string userInput = Ui.GetValidInput("y", "n");
                if (userInput.ToLower().Equals("n"))
                {
                    stillSelecting = false;
                }

            } while (stillSelecting);

            PrintResults(queryResult);
            return stillSelecting;
        }

        private bool GetMotorcyclesBy()
        {
            List<string> mcProps = Motorcycle.GetMotorcyclePropertyNames();
            mcProps.RemoveAt(1);
            List<Vehicle> queryResult = [];
            bool stillSelecting = true;
            bool hasGarageBeenQueried = false;

            do
            {
                Ui.PrintLine("Which property would you like to find by?");
                int count = 1;
                foreach (var item in mcProps)
                {
                    Ui.PrintLine($"{count}. {item}");
                    count++;
                }
                Ui.PrintLine($"{count}. No specific property.");
                int option = Ui.GetValidInput(count);
                Ui.PrintLine("What would you like that property to be?");

                switch (option)
                {
                    case 1:
                        //CylinderVolume
                        int cylinderVolume = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IMotorcycle mc
                            && mc.CylinderVolume == cylinderVolume).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IMotorcycle mc
                            && mc.CylinderVolume == cylinderVolume).ToList();
                        }
                        break;
                    case 2:
                        //NumOfWheels
                        int numWheels = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IMotorcycle mc
                            && mc.NumOfWheels == numWheels).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null && v is Motorcycle mc && mc.NumOfWheels == numWheels).ToList();
                        }
                        break;
                    case 3:
                        //Color
                        string color = Ui.GetInput();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IMotorcycle mc
                            && mc.Color == color).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IMotorcycle mc
                            && mc.Color == color).ToList();
                        }
                        break;
                    case 4:
                        queryResult = Garage.Where(v => v != null
                        && v is IMotorcycle motorcycle).ToList();
                        break;
                }
                Ui.PrintLine("Would you like to add another property to filter on?");
                string userInput = Ui.GetValidInput("y", "n");
                if (userInput.ToLower().Equals("n"))
                {
                    stillSelecting = false;
                }
            } while (stillSelecting);

            PrintResults(queryResult);
            return stillSelecting;
        }
        private bool GetBussesBy()
        {
            List<string> busProps = Bus.GetBusPropertyNames();
            busProps.RemoveAt(1);
            List<Vehicle> queryResult = [];
            bool stillSelecting = true;
            bool hasGarageBeenQueried = false;
            do
            {
                Ui.PrintLine("Which property would you like to find by?");
                int count = 1;
                foreach (var item in busProps)
                {
                    Ui.PrintLine($"{count}. {item}");
                    count++;
                }
                Ui.PrintLine($"{count}. No specific property.");
                int option = Ui.GetValidInput(count);
                Ui.PrintLine("What would you like that property to be?");

                switch (option)
                {
                    case 1:
                        //NumOfSeats
                        int numSeats = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IBus bus
                            && bus.NumOfSeats == numSeats).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IBus bus
                            && bus.NumOfSeats == numSeats).ToList();
                        }
                        break;
                    case 2:
                        //NumOfWheels
                        int numWheels = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IBus bus
                            && bus.NumOfWheels == numWheels).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IBus bus
                            && bus.NumOfWheels == numWheels).ToList();
                        }
                        break;
                    case 3:
                        //Color
                        string color = Ui.GetInput();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is IBus bus
                            && bus.Color == color).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is IBus bus
                            && bus.Color == color).ToList();
                        }
                        break;
                    case 4:
                        queryResult = Garage.Where(v => v != null
                        && v is IBus bus).ToList();
                        break;
                    default:
                        Ui.PrintLine("The input is not valid");
                        break;
                }
                Ui.PrintLine("Would you like to add another property to filter on?");
                string userInput = Ui.GetValidInput("y", "n");
                if (userInput.ToLower().Equals("n"))
                {
                    stillSelecting = false;
                }
            } while (stillSelecting);

            PrintResults(queryResult);
            return stillSelecting;
        }

        private bool GetCarsBy()
        {
            List<string> carProps = Car.GetCarPropertyNames();
            carProps.RemoveAt(1);
            List<Vehicle> queryResult = [];
            bool stillSelecting = true;
            bool hasGarageBeenQueried = false;
            do
            {
                Ui.PrintLine("Which property would you like to find by?");
                int count = 1;
                foreach (var item in carProps)
                {
                    Ui.PrintLine($"{count}. {item}.");
                    count++;
                }
                Ui.PrintLine($"{count}. No specific property.");
                int option = Ui.GetValidInput(count);
                switch (option)
                {
                    case 1:
                        //FuelType
                        string fuelType = Ui.GetValidInput("gasoline", "diesel");
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is ICar car
                            && car.FuelType == fuelType).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is ICar car
                            && car.FuelType == fuelType).ToList();
                        }
                        break;
                    case 2:
                        //NumOfWheels
                        int numWheels = Ui.GetValidInt();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is ICar car
                            && car.NumOfWheels == numWheels).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is ICar car
                            && car.NumOfWheels == numWheels).ToList();
                        }
                        break;
                    case 3:
                        //Color
                        string color = Ui.GetInput();
                        if (queryResult.Count == 0 && !hasGarageBeenQueried)
                        {
                            queryResult = Garage.Where(v => v != null
                            && v is ICar car
                            && car.Color == color).ToList();
                            hasGarageBeenQueried = true;
                        }
                        else
                        {
                            queryResult = queryResult.Where(v => v != null
                            && v is ICar car
                            && car.Color == color).ToList();
                        }
                        break;
                    case 4:
                        //No specific
                        queryResult = Garage.Where(v => v != null
                        && v is ICar car).ToList();
                        break;

                }
                Ui.PrintLine("Would you like to add another property to filter on?");
                string userInput = Ui.GetValidInput("y", "n");
                if (userInput.ToLower().Equals("n"))
                {
                    stillSelecting = false;
                }

            } while (stillSelecting);

            PrintResults(queryResult);
            return stillSelecting;
        }

        private bool GetVehiclesBy()
        {
            List<string> vehicleProps = Vehicle.GetPropertyNames();
            vehicleProps.RemoveAt(0);

            List<Vehicle> queryResult = [];
            bool stillSelecting = true;
            bool hasGarageBeenQueried = false;

            Ui.PrintLine("Select properties would you like to find by?");
            for (int i = 0; i < vehicleProps.Count; i++)
            {
                Ui.PrintLine($"{i + 1}. {vehicleProps[i]}");
            }

            Ui.PrintLine("Select properties would you like to find by?");
            for (int i = 0; i < vehicleProps.Count; i++)
            {
                Ui.PrintLine($"{i + 1}. {vehicleProps[i]}");
            }
            int option = Ui.GetValidInput(vehicleProps.Count);
            Ui.PrintLine("What would you like that property to be?");

            switch (option)
            {
                case 1:
                    int wheelCount = Ui.GetValidInt();
                    if (queryResult.Count == 0 && hasGarageBeenQueried == false)
                    {
                        queryResult = Garage.Where(v => v != null && v.NumOfWheels == wheelCount).ToList();
                        hasGarageBeenQueried = true;
                    }
                    else
                    {
                        queryResult = queryResult.Where(v => v != null && v.NumOfWheels == wheelCount).ToList();
                    }
                    break;
                case 2:
                    string color = Ui.GetInput();
                    if (queryResult.Count == 0 && hasGarageBeenQueried == false)
                    {
                        queryResult = Garage.Where(v => v != null && v.Color.ToLower() == color.ToLower()).ToList();
                        hasGarageBeenQueried = true;
                    }
                    else
                    {
                        queryResult = queryResult.Where(v => v != null && v.Color == color).ToList();
                    }
                    break;
                default:
                    Ui.PrintLine("The option is not available");
                    break;
            }
            Ui.PrintLine("Would you like to add another property to filter on?");
            string userInput = Ui.GetValidInput("y", "n");
            if (userInput.ToLower().Equals("n"))
                stillSelecting = false;

            return stillSelecting;
        }
    }
}
