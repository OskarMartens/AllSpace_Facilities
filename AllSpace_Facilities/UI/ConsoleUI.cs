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

        public string GetInput(string input)
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
    }
}
