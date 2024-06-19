using AllSpace_Facilities.Entities;
using AllSpace_Facilities.Handler;
using AllSpace_Facilities.UI;

namespace AllSpace_Facilities
{
    internal class Manager
    {
        private readonly IUI _ui;

        public Manager(IUI ui)
        {
            _ui = ui;
        }

        public void Run()
        {
            MainMenuSimple();
        }

        public void MainMenuSimple()
        {
            MainMenuHeader();
            int garageCapacity = GetGarageCapacity();
            bool toPopulate = MainMenuSelectionAlt();
            if (toPopulate)
            {
                GarageHandler commonGarageHandler = new(new Garage<Vehicle>(garageCapacity), _ui);
                commonGarageHandler.PopulateGarage(garageCapacity);
                commonGarageHandler.Run();
            }
        }

        private bool MainMenuSelectionAlt()
        {
            _ui.PrintLine("Would you like to populate the garage with some already defined vehicles? y or n");
            string userInput = _ui.GetValidInput("y", "n");
            if (userInput.Equals("y"))
                return true;
            return false;
        }

        private void MainMenuHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _ui.PrintLine("Welcome to the AllSpace Facilities garage application");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private int GetGarageCapacity()
        {
            _ui.PrintLine("What should the capacity of the garage be?");
            int choice = _ui.GetValidInt();
            return choice;
        }
    }
}
