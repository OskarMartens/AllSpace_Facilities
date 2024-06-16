using AllSpace_Facilities.Entities;
using AllSpace_Facilities.UI;

namespace AllSpace_Facilities
{
    internal class Manager
    {
        private IUI _ui;

        public Manager(IUI ui)
        {
            _ui = ui;
        }

        public void Run()
        {
            MainMenuText();
            List<IGarage> garages = new List<IGarage>();

            AddGarage<Car>(garages, 12);

            Garage<Vehicle> genericGarage = new Garage<Vehicle>(GetAvailableId(garages), parkingCapacity: 13);
            garages.Add(genericGarage);

            Garage<Bus> busGarage = new Garage<Bus>(GetAvailableId(garages), parkingCapacity: 14);
            garages.Add(busGarage);

            Car car = new Car("gasoline", "ABC 123", numOfWheels: 4);
            Bus bus = new Bus(40, "XYZ 789", 6);

            genericGarage.AddVehicle(car);
            busGarage.AddVehicle(bus);

            foreach (var garage in garages)
            {
                Console.WriteLine(garage.Id);
            }
        }

        void AddGarage<T>(List<IGarage> garages, int parkingCapacity) where T : Vehicle, new()
        {
            Garage<T> garage = new Garage<T>(GetAvailableId(garages), parkingCapacity);
            garages.Add(garage);
        }


        public int GetAvailableId(IEnumerable<IGarage> garages)
        {
            return garages.Any() ? garages.Max(garage => garage.Id) + 1 : 1;
        }

        public void MainMenuText()
        {
            _ui.PrintLine("Welcome to the AllSpace Facilities garage application");
            _ui.PrintWithDash("This application lets you manage different garages and vehicles within them.");
            _ui.PrintLine("You can create garages with parking capacity.");
            _ui.PrintLine("You can also edit number of the parking spots in a garage.");

            _ui.PrintLine("You can also view garages and the vehicles parked there.");
            _ui.PrintWithDash("And you can delete a garage if there are no vehicles within it.");
            _ui.PrintLine("In the separate garages you can list all vehicles that are parked there.");
            _ui.PrintWithDash("You can list them through various attributes as well.");
            _ui.PrintLine("Here is the menu:");
            _ui.PrintLine("1. List all garages.");
            _ui.PrintLine("2. Create garage.");
            _ui.PrintLine("3. Edit garage and vehicles within");
            _ui.PrintLine("4. Delete garage. Note that the garage has to be empty");

        }
    }
}
