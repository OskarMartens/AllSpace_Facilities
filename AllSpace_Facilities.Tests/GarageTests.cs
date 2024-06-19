using AllSpace_Facilities.Entities;
using AllSpace_Facilities.UI;
using Moq;

namespace AllSpace_Facilities.Tests
{
    public class GarageTests
    {

        [Fact]
        public void AddVehicle_ShouldAddVehicleToGarage()
        {
            // Arrange
            var mockUI = new Mock<IUI>();
            int capacity = 2;
            var garage = new Garage<Vehicle>(capacity);
            garage.Ui = mockUI.Object;

            var vehicle = new Car("diesel", "ABC 123", "red");

            // Act
            garage.AddVehicle(vehicle);

            // Assert
            Assert.Contains(vehicle, garage.Vehicles);
            mockUI.Verify(ui => ui.PrintLine(It.Is<string>(s => s.Contains($"You successfully parked a {vehicle.GetType().Name} with the license plate: {vehicle.LicensePlate}"))), Times.Once);
        }

        [Fact]
        public void AddVehicle_ShouldThrowException_WhenGarageIsFull()
        {
            // Arrange
            var mockUI = new Mock<IUI>();
            int capacity = 1;
            var garage = new Garage<Vehicle>(capacity);
            garage.Ui = mockUI.Object;

            var vehicle1 = new Car("diesel", "ABC 123", "red");
            var vehicle2 = new Car("gasoline", "DEF 456", "blue");

            // Act
            garage.AddVehicle(vehicle1);

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => garage.AddVehicle(vehicle2));
            Assert.Equal("Garage is full.", exception.Message);
        }

        [Fact]
        public void GetGarageInfo_ShouldPrintMessage_WhenGarageIsEmpty()
        {
            // Arrange
            var mockUI = new Mock<IUI>();
            int capacity = 2;
            var garage = new Garage<Vehicle>(capacity);
            garage.Ui = mockUI.Object;

            // Act
            garage.GetGarageInfo();

            // Assert
            mockUI.Verify(ui => ui.PrintLine("There are no vehicles in the garage"), Times.Once);
        }

        [Fact]
        public void GetGarageInfo_ShouldPrintVehicleTypesAndCounts_WhenGarageHasVehicles()
        {
            // Arrange
            var mockUI = new Mock<IUI>();
            int capacity = 3;
            var garage = new Garage<Vehicle>(capacity);
            garage.Ui = mockUI.Object;

            var car1 = new Car("diesel", "ABC 123", "red");
            var car2 = new Car("gasoline", "DEF 456", "blue");
            var bus = new Bus(50, "GHI789", 8, "yellow");

            garage.AddVehicle(car1);
            garage.AddVehicle(car2);
            garage.AddVehicle(bus);

            // Act
            garage.GetGarageInfo();

            // Assert
            mockUI.Verify(ui => ui.PrintLine("There are:"), Times.Once);
            mockUI.Verify(ui => ui.PrintLine("There are 2 cars in the garage"), Times.Once);
            mockUI.Verify(ui => ui.PrintLine("There are 1 buss in the garage"), Times.Once);
        }

        //[Fact]
        //public void GetByLicensePlate_ShouldPrintVehicleStats_WhenVehicleExists()
        //{
        //    // Arrange
        //    var mockUI = new Mock<IUI>();
        //    var garage = new Garage<Vehicle>(5);
        //    garage.Ui = mockUI.Object;

        //    var car1 = new Car("diesel", "ABC 123", "blue");


        //    garage.AddVehicle(car1);

        //    // Act
        //    garage.GetByLicensePlate("ABC 123");

        //    // Assert
        //    //IUI.PrintLine("You successfully parked a Car with the license plate: ABC 123");
        //    mockUI.Verify(ui => ui.PrintLine("You successfully parked a Car with the license plate: ABC 123"), Times.Once);
        //    mockUI.Verify(ui => ui.PrintLine("Vehicle type:\t\tCar\nFuelType:\t\tdiesel\nLicense plate:\t\tABC 123\r\nNumber of wheels:\t4\nColor:\t\tblue"), Times.Once);
        //    mockUI.Verify(ui => ui.PrintLine(""), Times.Once);
        //}

        [Fact]
        public void GetByLicensePlate_ShouldPrintNotFoundMessage_WhenVehicleDoesNotExist()
        {
            // Arrange
            var mockUI = new Mock<IUI>();
            var garage = new Garage<Vehicle>(5);
            garage.Ui = mockUI.Object;

            var car1 = new Car("diesel", "ABC123", "red");
            var car2 = new Car("gasoline", "DEF456", "blue");

            garage.AddVehicle(car1);
            garage.AddVehicle(car2);

            // Act
            garage.GetByLicensePlate("XYZ789");

            // Assert
            mockUI.Verify(ui => ui.PrintLine("No vehicles with license plate: XYZ789 found."), Times.Once);
        }

        [Fact]
        public void RemoveVehicle_ShouldPrintNotFoundMessage_WhenVehicleDoesNotExist()
        {
            // Arrange
            var mockUI = new Mock<IUI>();
            var garage = new Garage<Vehicle>(5);
            garage.Ui = mockUI.Object;

            var car1 = new Car("diesel", "ABC 123", "red");
            var car2 = new Car("gasoline", "DEF 456", "blue");

            garage.AddVehicle(car1);
            garage.AddVehicle(car2);

            // Act
            garage.RemoveVehicle("XYZ 789");
            // Assert
            mockUI.Verify(ui => ui.PrintLine("No vehicle with the license plate: XYZ 789 was found"), Times.Once);
        }
    }
}