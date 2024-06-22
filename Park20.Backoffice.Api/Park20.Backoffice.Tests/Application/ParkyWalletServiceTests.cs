// using Moq;
// using NUnit.Framework;
// using Park20.Backoffice.Application.Services;
// using Park20.Backoffice.Core.Domain.User;
// using Park20.Backoffice.Core.IRepositories;

// namespace Park20.Backoffice.Tests.Application
// {
//     [TestFixture]
//     internal class ParkyWalletServiceTests
//     {
//         [Test]
//         public async Task BulkParkyTest()
//         {
//             // Arrange
//             var mockParkyCoinsConfigurationRepository = new Mock<IParkyCoinsConfigurationRepository>();
//             var mockParkyWalletRepository = new Mock<IParkyWalletRepository>();
//             var mockUserRepository = new Mock<IUserRepository>();
//             mockParkyCoinsConfigurationRepository.Setup(repo => repo.GetBulkValue()).ReturnsAsync(5);
//             mockUserRepository.Setup(repo => repo.GetUserByUsername(It.IsAny<string>())).ReturnsAsync(new Customer { ParkyWalletId = 5 });
//             mockParkyWalletRepository.Setup(repo => repo.BulkAdd(It.IsAny<List<int>>(), It.IsAny<int>())).ReturnsAsync(true);
//             var parkyWalletService = new ParkyWalletService(mockParkyCoinsConfigurationRepository.Object, mockParkyWalletRepository.Object, mockUserRepository.Object);

//             // Act
//             var result = await parkyWalletService.BulkParky(["1", "2"]);

//             // Assert
//             Assert.IsTrue(result);
//         }

//         [Test]
//         public async Task DynamicValuesTest()
//         {
//             // Arrange
//             var mockParkyCoinsConfigurationRepository = new Mock<IParkyCoinsConfigurationRepository>();
//             var mockParkyWalletRepository = new Mock<IParkyWalletRepository>();
//             var mockUserRepository = new Mock<IUserRepository>();
//             mockParkyCoinsConfigurationRepository.Setup(repo => repo.GetBulkValue()).ReturnsAsync(5);
//             mockParkyCoinsConfigurationRepository.Setup(repo => repo.GetParkingValue()).ReturnsAsync(1);
//             mockParkyCoinsConfigurationRepository.Setup(repo => repo.GetNewCustomerValue()).ReturnsAsync(1);
//             mockUserRepository.Setup(repo => repo.GetUserByUsername(It.IsAny<string>())).ReturnsAsync(new Customer { ParkyWalletId = 5 });
//             mockParkyWalletRepository.Setup(repo => repo.BulkAdd(It.IsAny<List<int>>(), It.IsAny<int>())).ReturnsAsync(true);
//             var parkyWalletService = new ParkyWalletService(mockParkyCoinsConfigurationRepository.Object, mockParkyWalletRepository.Object, mockUserRepository.Object);
//             parkyWalletService.GetParkyValues();

//             // Act
//             var result = await parkyWalletService.UpdateBulkValue(1);
//             var result1 = await parkyWalletService.UpdateParkingValue(1);
//             var result2 = await parkyWalletService.UpdateNewCustomerValue(1);
//             var result3 = await parkyWalletService.GetParkyValues();

//             // Assert
//             Assert.IsTrue(result);
//             Assert.IsTrue(result1);
//             Assert.IsTrue(result2);
//             Assert.True(result3.IndexOf(1) != -1);
//         }
//     }
// }
