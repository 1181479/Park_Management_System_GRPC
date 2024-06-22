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
//     public class ParkLogRepositoryTests
//     {
//         [Test]
//         public async Task CreateParkLog()
//         {
//             // Arrange
//             var repo = new Mock<IParkLogRepository>();
//             ParkLog parklog = new ParkLog();
//             var time = DateTime.Now;
//             parklog.StartTime = time;
//             repo.Setup(r => r.CreateParkLog(It.IsAny<ParkLog>(), It.IsAny<DateTime>())).ReturnsAsync(parklog);
//             // Act
//             var result = await repo.Object.CreateParkLog(new ParkLog(), time);

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(time, result.StartTime);
//         }

//         [Test]
//         public async Task UpdateParkLogWithEndingTime()
//         {
//             // Arrange
//             var repo = new Mock<IParkLogRepository>();
//             ParkLog parklog = new ParkLog();
//             var time = DateTime.Now;
//             parklog.StartTime = time;
//             repo.Setup(r => r.UpdateParkLogWithEndingTime(It.IsAny<ParkLog>())).ReturnsAsync(true);
//             // Act
//             var result = await repo.Object.UpdateParkLogWithEndingTime(new ParkLog());

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.True(result);
//         }

//         [Test]
//         public async Task UpdateAvailableParkingSpots()
//         {
//             // Arrange
//             var repo = new Mock<IParkLogRepository>();
//             ParkLog parklog = new ParkLog();
//             var time = DateTime.Now;
//             parklog.StartTime = time;
//             repo.Setup(r => r.UpdateAvailableParkingSpots(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(true);
//             // Act
//             var result = await repo.Object.UpdateAvailableParkingSpots("parkName", "cc-00-22", true);

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.True(result);
//         }

//         [Test]
//         public async Task GetParkLog()
//         {
//             // Arrange
//             var repo = new Mock<IParkLogRepository>();
//             ParkLog parklog = new ParkLog();
//             var time = DateTime.Now;
//             parklog.StartTime = time;
//             repo.Setup(r => r.GetParkLog(It.IsAny<string>())).ReturnsAsync(parklog);
//             // Act
//             var result = await repo.Object.GetParkLog("cc-00-22");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(time, result.StartTime);
//         }

//     }
// }