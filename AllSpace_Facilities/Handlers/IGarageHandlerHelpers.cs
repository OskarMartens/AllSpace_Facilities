using AllSpace_Facilities.Entities;
using AllSpace_Facilities.Enums;

namespace AllSpace_Facilities.Handlers
{
    public interface IGarageHandlerHelpers
    {
        IGarage<Vehicle> Garage { get; }
        IEnumerable<VehicleTypes> VehicleTypes { get; }

        int AvailableSpotsCount();
        void GetAllVehicles();
        void GetByLicensePlate();
        void GetByProperties();
        void GetGarageInfo();
        string GetUniqueLicensePlate();
        bool IsGarageFull();
        void NoVehicles();
        void PrintAvailableSpots();
    }
}