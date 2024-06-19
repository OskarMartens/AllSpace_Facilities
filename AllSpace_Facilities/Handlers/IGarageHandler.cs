namespace AllSpace_Facilities.Handler
{
    public interface IGarageHandler
    {
        void CreateVehicle();
        void GarageHandlerMenu();
        void PopulateGarage(int garageCapacity);
        void Run();
    }
}