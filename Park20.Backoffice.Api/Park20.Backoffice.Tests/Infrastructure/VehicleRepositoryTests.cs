// using Castle.Core.Configuration;
// using Microsoft.Extensions.Configuration;
// using Moq;
// using NUnit.Framework;
// using Park20.Backoffice.Core.Domain.Park;
// using Park20.Backoffice.Core.Domain;
// using Park20.Backoffice.Core.Domain.User;
// using Park20.Backoffice.Core.IRepositories;
// using Park20.Backoffice.Infrastructure.Repositories;
// using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
// using Castle.Core.Resource;

// namespace Park20.Backoffice.Tests
// {
//     [TestFixture]
//     public class VehicleRepositoryTests
//     {
//         [Test]
//         public async Task AddVehicle()
//         {
//             // Arrange
//             var repo = new Mock<IVehicleRepository>();
//             Vehicle vehicle = new Vehicle();
//             vehicle.LicensePlate = "cc-00-22";
//             repo.Setup(r => r.AddVehicle(It.IsAny<Vehicle>(), It.IsAny<string>())).ReturnsAsync(vehicle);
//             // Act
//             var result = await repo.Object.AddVehicle(new Vehicle(), "username");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("cc-00-22", result.LicensePlate);
//         }

//         [Test]
//         public async Task GetVehicle()
//         {
//             // Arrange
//             var repo = new Mock<IVehicleRepository>();
//             Vehicle vehicle = new Vehicle();
//             vehicle.LicensePlate = "cc-00-22";
//             repo.Setup(r => r.GetVehicle(It.IsAny<string>())).ReturnsAsync(vehicle);
//             // Act
//             var result = await repo.Object.GetVehicle("cc-00-22");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("cc-00-22", result.LicensePlate);
//         }
        
//         [Test]
//         public async Task GetVehicles()
//         {
//             // Arrange
//             var repo = new Mock<IVehicleRepository>();
//             Vehicle vehicle = new Vehicle();
//             vehicle.LicensePlate = "cc-00-22";
//             repo.Setup(r => r.GetAllFromUser(It.IsAny<string>())).ReturnsAsync(new List<Vehicle> { vehicle });
//             // Act
//             var result = await repo.Object.GetAllFromUser("username");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("cc-00-22", result.ToArray()[0].LicensePlate);
//         }
//     }
// }