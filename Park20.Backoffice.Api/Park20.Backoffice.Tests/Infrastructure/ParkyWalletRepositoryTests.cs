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
// using Park20.Backoffice.Application.Services;

// namespace Park20.Backoffice.Tests
// {
//     [TestFixture]
//     public class ParkyWalletRepositoryTests
//     {
//         [Test]
//         public async Task AddVehicle()
//         {
//             // Arrange
//             var repo = new Mock<IParkyWalletRepository>();
//             repo.Setup(repo => repo.BulkAdd(It.IsAny<List<int>>(), It.IsAny<int>())).ReturnsAsync(true);

//             // Act
//             var result = await repo.Object.BulkAdd([1, 2, 3], 5);

//             // Assert
//             Assert.IsTrue(result);
//         }
//     }
// }