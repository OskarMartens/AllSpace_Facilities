using AllSpace_Facilities.Enums;

namespace AllSpace_Facilities.UI
{
    public class ConsoleUI : IUI
    {

        public void PrintLine(string message)
        {
            Console.WriteLine(message);
        }
        public void Print(string message)
        {
            Console.Write(message);
        }

        public string GetInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public void PrintWithDash(string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
        }



        public int GetValidInput(int rangeMax)
        {
            bool isNumeric = int.TryParse(Console.ReadLine(), out int choice);
            while (!isNumeric || !Enumerable.Range(1, rangeMax).Contains(choice))
            {
                Console.WriteLine($"The input is not valid. Pick a valid number between 1 and {rangeMax}");
                isNumeric = int.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
        }

        public int GetValidInt()
        {
            bool isNumeric = int.TryParse(Console.ReadLine(), out int choice);
            while (!isNumeric)
            {
                Console.WriteLine($"The input is not a number");
                isNumeric = int.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
        }

        public string GetValidInput(string v1, string v2)
        {
            bool isValid = false;
            string userInput = string.Empty;
            while (!isValid)
            {
                Console.WriteLine($"Please select between {v1} or {v2}");
                userInput = Console.ReadLine() ?? " ";
                if (userInput.ToLower().Equals(v1) || userInput.ToLower().Equals(v2))
                    isValid = true;
            }
            return userInput.ToLower();
        }

        public void ListVehicleTypes()
        {
            IEnumerable<VehicleTypes> vehicleTypes
                        = Enum.GetValues(typeof(VehicleTypes)).Cast<VehicleTypes>().ToList();

            Console.WriteLine("What type of garage would you like to create");
            foreach (var vehicleType in vehicleTypes)
            {
                Console.WriteLine($"{(int)vehicleType}. {vehicleType}");
            }
        }
    }
}
