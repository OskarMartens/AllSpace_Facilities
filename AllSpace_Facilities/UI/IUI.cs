namespace AllSpace_Facilities.UI
{
    public interface IUI
    {
        string GetInput(string input);
        void PrintLine(string message);
        void Print(string message);

        void PrintWithDash(string message);
    }
}