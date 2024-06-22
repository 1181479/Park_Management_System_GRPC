// using Moq;
// using NUnit.Framework;
// using Park20.Backoffice.Core.Domain;
// using Park20.Backoffice.Core.IRepositories;
// using Park20.Backoffice.Infrastructure.Repositories;
// using Park20.Backoffice.Application.Services;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using HttpRequest = Park20.Backoffice.Application.Requests.HttpRequests;


// namespace Park20.Backoffice.Tests.Application
// {
//     [TestFixture]
//     public class ParkLogServiceTests
//     {
//         [Test]
//         public async Task StartingCountingTimeOperation_Should_Call_CreateParkLog()
//         {
//             // Arrange
//             var mockRepository = new Mock<IParkLogRepository>();
//             var parkLogService = new ParkLogService(mockRepository.Object); // Usando Mock.Of<T> para criar o mock.
//             var licensePlate = "ABC123";
//             var parkName = "ParkingLot";

//             // Act
//             await parkLogService.StartingCountingTimeOperation(licensePlate, parkName);

//             // Assert
//             mockRepository.Verify(repo => repo.CreateParkLog(It.IsAny<ParkLog>(), It.IsAny<DateTime>()), Times.Once);
//         }
//         [Test]
//         public async Task StopCountingTimeOperation_Should_Call_UpdateParkLogWithEndingTime()
//         {
//             // Arrange
//             var mockRepository = new Mock<IParkLogRepository>();
//             var parkLogService = new ParkLogService(mockRepository.Object);
//             var licensePlate = "ABC123";
//             var parkName = "ParkingLot";

//             // Act
//             await parkLogService.StopCountingTimeOperation(licensePlate, parkName);

//             // Assert
//             mockRepository.Verify(repo => repo.UpdateParkLogWithEndingTime(It.IsAny<ParkLog>()), Times.Once);
//         }

//         [Test]
//         public async Task UpdateAvailableParkingSpots_Should_Call_UpdateAvailableParkingSpots()
//         {
//             // Arrange
//             var mockRepository = new Mock<IParkLogRepository>();
//             var parkLogService = new ParkLogService(mockRepository.Object);
//             var parkName = "ParkingLot";
//             var licensePlate = "ABC123";
//             var isEntrance = true;

//             // Act
//             await parkLogService.UpdateAvailableParkingSpots(parkName, licensePlate, isEntrance);

//             // Assert
//             mockRepository.Verify(repo => repo.UpdateAvailableParkingSpots(parkName, licensePlate, isEntrance), Times.Once);
//         }
       
//         [Test]
//         public async Task StartingCountingTimeOperation_Should_Call_CreateParkLog_WithCorrectParameters()
//         {
//             // Arrange
//             var mockRepository = new Mock<IParkLogRepository>();
//             var parkLogService = new ParkLogService(mockRepository.Object);
//             var licensePlate = "ABC123";
//             var parkName = "ParkingLot";

//             // Act
//             await parkLogService.StartingCountingTimeOperation(licensePlate, parkName);

//             // Assert
//             mockRepository.Verify(repo => repo.CreateParkLog(It.Is<ParkLog>(p => p.LicensePlate == licensePlate && p.ParkName == parkName), It.IsAny<DateTime>()), Times.Once);
//         }

//         [Test]
//         public async Task StopCountingTimeOperation_Should_Call_UpdateParkLogWithEndingTime_WithCorrectParameters()
//         {
//             // Arrange
//             var mockRepository = new Mock<IParkLogRepository>();
//             var parkLogService = new ParkLogService(mockRepository.Object);
//             var licensePlate = "ABC123";
//             var parkName = "ParkingLot";

//             // Act
//             await parkLogService.StopCountingTimeOperation(licensePlate, parkName);

//             // Assert
//             mockRepository.Verify(repo => repo.UpdateParkLogWithEndingTime(It.Is<ParkLog>(p => p.LicensePlate == licensePlate && p.ParkName == parkName)), Times.Once);
//         }


//     }
// }
