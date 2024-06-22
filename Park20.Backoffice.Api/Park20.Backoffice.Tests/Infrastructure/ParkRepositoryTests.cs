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
// using Park20.Backoffice.Core.Dtos.Requests;
// using Park20.Backoffice.Application.Mappers;

// namespace Park20.Backoffice.Tests
// {
//     [TestFixture]
//     public class ParkRepositoryTests
//     {
//         [Test]
//         public async Task GetParkByName()
//         {
//             // Arrange
//             var repo = new Mock<IParkRepository>();
//             Park park = new Park();
//             park.ParkName = "parkName";
//             repo.Setup(r => r.GetParkByName(It.IsAny<string>())).ReturnsAsync(park);
//             // Act
//             var result = await repo.Object.GetParkByName("parkName");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("parkName", result.ParkName);
//         }

//         [Test]
//         public async Task GetParkingSpotsByParkName()
//         {
//             // Arrange
//             var repo = new Mock<IParkRepository>();
//             Park park = new Park();
//             park.ParkName = "parkName";
//             ParkingSpot parkingSpot = new ParkingSpot();
//             parkingSpot.FloorNumber = 2;
//             parkingSpot.ParkingSpotId = 50;
//             repo.Setup(r => r.GetParkingSpotsByParkName(It.IsAny<string>())).ReturnsAsync(new List<ParkingSpot> { parkingSpot });
//             // Act
//             var result = await repo.Object.GetParkingSpotsByParkName("parkName");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(2, result.ToArray()[0].FloorNumber);
//             Assert.AreEqual(50, result.ToArray()[0].ParkingSpotId);
//         }

//         [Test]
//         public async Task GetParkingSpotsAvailableByParkName()
//         {
//             // Arrange
//             var repo = new Mock<IParkRepository>();
//             Park park = new Park();
//             park.ParkName = "parkName";
//             ParkingSpot parkingSpot = new ParkingSpot();
//             parkingSpot.FloorNumber = 2;
//             parkingSpot.ParkingSpotId = 50;
//             repo.Setup(r => r.GetParkingSpotsAvailableByParkName(It.IsAny<string>())).ReturnsAsync(new List<ParkingSpot> { parkingSpot });
//             // Act
//             var result = await repo.Object.GetParkingSpotsAvailableByParkName("parkName");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(2, result.ToArray()[0].FloorNumber);
//             Assert.AreEqual(50, result.ToArray()[0].ParkingSpotId);
//         }

//         [Test]
//         public async Task GetAllParks()
//         {
//             // Arrange
//             var repo = new Mock<IParkRepository>();
//             Park park = new Park();
//             park.ParkName = "parkName";
//             ParkingSpot parkingSpot = new ParkingSpot();
//             parkingSpot.FloorNumber = 2;
//             parkingSpot.ParkingSpotId = 50;
//             repo.Setup(r => r.GetAllParks()).ReturnsAsync(new List<Park> { park });
//             // Act
//             var result = await repo.Object.GetAllParks();

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("parkName", result.ToArray()[0].ParkName);
//         }

//         [Test]
//         public async Task UpdatePriceTable()
//         {
//             // Arrange
//             string parkName = "YourParkName";
//             double nightFee = 10.99;
//             DateTime initialDate = DateTime.Now;
//             List<LinePriceTableDto> linePrices = new List<LinePriceTableDto>
//             {
//                 new LinePriceTableDto(new PeriodDto("08:00:00", "12:00:00", new List<FractionsDto>
//                 {
//                     new FractionsDto(1, 15, VehicleType.Motocycle, 2),
//                     new FractionsDto(2, 30, VehicleType.Automobile, 1)
//                 }))
//             };
//             var updatePriceTableDto = new PriceTableDto(parkName, nightFee, initialDate, linePrices);
//             var repo = new Mock<IParkRepository>();
//             repo.Setup(r => r.UpdateParkPriceTable(parkName, nightFee, ParkMapper.ToPriceTableDomain(updatePriceTableDto))).ReturnsAsync(true);
//             // Act
//             var result = await repo.Object.UpdateParkPriceTable(parkName, nightFee, ParkMapper.ToPriceTableDomain(updatePriceTableDto));

//             // Assert
//             Assert.False(result);
//         }
//     }
// }