using AllSpace_Facilities;
using AllSpace_Facilities.UI;

{
    ConsoleUI _ui = new ConsoleUI();
    Manager manager = new Manager(_ui);
    manager.Run();
}
