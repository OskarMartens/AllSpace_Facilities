using AllSpace_Facilities.Entities;
using AllSpace_Facilities.Enums;
using AllSpace_Facilities.Handlers;
using AllSpace_Facilities.UI;

namespace AllSpace_Facilities.Handler
{
    public class GarageHandler : IGarageHandler
    {
        private IGarage<Vehicle> _garage;
        private IUI _ui;
        private IEnumerable<VehicleTypes> _vehicleTypes
            = Enum.GetValues(typeof(VehicleTypes)).Cast<VehicleTypes>().ToList();
        private IGarageHandlerHelpers helpers;

        public GarageHandler(IGarage<Vehicle> garage, IUI ui)
        {
            _garage = garage;
            _ui = ui;
            helpers = new GarageHandlerHelpers(ui, _vehicleTypes, _garage);
        }

        public void Run()
        {
            GarageHandlerMenu();
        }

        private bool ReRunMenu()
        {
            bool runMenu = false;
            _ui.PrintLine("Press any key to see the menu again.");
            _ui.GetInput();
            GarageHandlerMenu();
            return runMenu;
        }

        public void GarageHandlerMenu()
        {
            Console.Clear();
            _ui.PrintWithDash($"Welcome to the {_garage.GetVehicleType()} garage");

            HandlerChoice();
            int choice = _ui.GetValidInt();

            bool runLoop = true;
            do
            {
                switch (choice)
                {
                    case 1:
                        helpers.PrintAvailableSpots();
                        runLoop = ReRunMenu();
                        break;
                    case 2:
                        //add
                        CreateVehicle();
                        runLoop = ReRunMenu();
                        break;
                    case 3:
                        //Remove
                        RemoveVehicle();
                        runLoop = ReRunMenu();
                        break;
                    case 4:
                        //Find based on properties
                        helpers.GetByProperties();
                        runLoop = ReRunMenu();
                        break;
                    case 5:
                        //List all vehicles
                        helpers.GetAllVehicles();
                        runLoop = ReRunMenu();
                        break;
                    case 6:
                        //List all vehicle types and how many of each.
                        helpers.GetGarageInfo();
                        runLoop = ReRunMenu();
                        break;
                    case 7:
                        //Find by license plate
                        helpers.GetByLicensePlate();
                        runLoop = ReRunMenu();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        _ui.PrintLine("The input is not valid");
                        break;
                }
            } while (runLoop);
        }

        public void CreateVehicle()
        {
            if (helpers.IsGarageFull())
            {
                _ui.PrintLine("The garage is full so you cannot park any vehicles.");
                return;
            }

            else if (_garage.Any())
            {
                _ui.PrintLine("You cannot create any vehicles with these license plates");
                _garage.ToList().ForEach(i => _ui.PrintLine(i.LicensePlate));
            }

            _ui.PrintLine("What type of vehicle would you like to create?");
            _vehicleTypes.ToList().ForEach(vehicleType
                => Console.WriteLine($"{(int)vehicleType}. {vehicleType}"));

            int choice = _ui.GetValidInput(_vehicleTypes.Count());
            switch (choice)
            {
                case 1:
                    _ui.PrintLine("You cannot instantiate a vehicle.");
                    _ui.PrintLine("You have to pick a concrete example");
                    break;
                case 2:
                    CreateCar();
                    break;
                case 3:
                    CreateBus();
                    break;
                case 4:
                    CreateMotorcycle();
                    break;
                case 5:
                    CreateBoat();
                    break;
                case 6:
                    CreateAirplane();
                    break;
            }
        }

        private void CreateCar()
        {
            _ui.PrintLine("What is the license plate of the car?");
            string licensePlate = helpers.GetUniqueLicensePlate();
            _ui.PrintLine("What fuel type does the car use? Gasoline or diesel.");
            string fuelType = _ui.GetInput();
            _ui.PrintLine("What color is the car?");
            string vehicleColor = _ui.GetInput();
            Car car = new(fuelType, licensePlate, vehicleColor);
            _garage.AddVehicle(car);
        }

        private void CreateBus()
        {
            _ui.PrintLine("What is the license plate of the bus");
            string licensePlate = _ui.GetInput();
            _ui.PrintLine("What is the seat capacity of the bus");
            int seatCapacity = _ui.GetValidInt();
            _ui.PrintLine("How many wheels does the bus have?");
            int wheelCount = _ui.GetValidInt();
            _ui.PrintLine("What is the color of the bus?");
            string color = _ui.GetInput();
            Bus bus = new(wheelCount, licensePlate, seatCapacity, color);
            _garage.AddVehicle(bus);
        }

        private void CreateMotorcycle()
        {
            _ui.PrintLine("What is the license plate of the motorcycle");
            string licensePlate = _ui.GetInput();
            _ui.PrintLine("What is the cylinder volume of the motorcycle?");
            int cylinderVolume = _ui.GetValidInt();
            _ui.PrintLine("How many wheels does the motorcycle have?");
            int wheelCount = _ui.GetValidInt();
            _ui.PrintLine("What is the color of the motorcycle?");
            string color = _ui.GetInput();
            Motorcycle mc = new(cylinderVolume, licensePlate, wheelCount, color);
            _garage.AddVehicle(mc);
        }

        private void CreateBoat()
        {
            _ui.PrintLine("What is the license plate of the boat");
            string licensePlate = _ui.GetInput();
            _ui.PrintLine("What is the length of the boat?");
            int length = _ui.GetValidInt();
            _ui.PrintLine("What is the color of the boat?");
            string color = _ui.GetInput();
            Boat boat = new(length, licensePlate, color);
            _garage.AddVehicle(boat);
        }

        private void CreateAirplane()
        {
            _ui.PrintLine("What is the license plate of the airplane");
            string licensePlate = _ui.GetInput();
            _ui.PrintLine("How many engines does the airplane have?");
            int engineCount = _ui.GetValidInt();
            _ui.PrintLine("How many wheels does the airplane have?");
            int wheelCount = _ui.GetValidInt();
            _ui.PrintLine("What is the color of the airplane?");
            string color = _ui.GetInput();
            Airplane airplane = new(engineCount, licensePlate, wheelCount, color);
            _garage.AddVehicle(airplane);
        }

        private void RemoveVehicle()
        {
            _ui.PrintLine("Enter the license plate of the vehicle that you would like to remove.");
            string licensePlate = _ui.GetInput();
            _garage.RemoveVehicle(licensePlate);
        }

        private void HandlerChoice()
        {
            _ui.PrintLine("1. Get current available parking spots.");
            _ui.PrintLine("2. Park a new vehicle.");
            _ui.PrintLine("3. Remove a vehicle (using license plate).");
            _ui.PrintLine("4. Find a specific vehicle based on properties.");
            _ui.PrintLine("5. List all vehicles.");
            _ui.PrintLine("6. List all vehicle types and how many there are of each type.");
            _ui.PrintLine("7. Find by license plate.");
            _ui.PrintLine("8. Close the application.");
        }
        public void PopulateGarage(int garageCapacity)
        {
            Car car1 = new Car("diesel", "HEJ 132", "red");
            Car car2 = new("gasoline", "GRT 901", "blue");
            Car car3 = new("diesel", "KLM 223", "green");

            Bus bus1 = new(20, "KRT 001", 4, "yellow");
            Bus bus2 = new(14, "HHR 010", 6, "black");
            Bus bus3 = new(20, "LRE 883", 4, "blue");

            Airplane airplane = new(4, "GHTTY", 8, "white");
            Motorcycle mc = new(1, "LKR 091", 2, "black");
            Boat boat = new(13, "BOT 764", "white");

            List<Vehicle> vehicles = new List<Vehicle>
                {
                    car1,
                    mc,
                    boat,
                    airplane,
                    bus1,
                    bus2,
                    bus3,
                    car2,
                    car3
                };

            int vehiclesToAdd = Math.Min(garageCapacity, vehicles.Count);

            for (int i = 0; i < vehiclesToAdd; i++)
            {
                _garage.AddVehicle(vehicles[i]);
            }
        }
    }
}
