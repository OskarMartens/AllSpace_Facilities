
namespace AllSpace_Facilities.UI
{
    public interface IUI
    {
        string GetInput();
        void PrintLine(string message);
        void Print(string message);
        void PrintWithDash(string message);
        int GetValidInput(int optionCount);
        int GetValidInt();
        void ListVehicleTypes();
        string GetValidInput(string v1, string v2);
    }
}