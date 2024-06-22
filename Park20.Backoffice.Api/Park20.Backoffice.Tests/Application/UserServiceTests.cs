// using Moq;
// using NUnit.Framework;
// using Park20.Backoffice.Application.Services;
// using Park20.Backoffice.Core.Domain.ParkyWallets;
// using Park20.Backoffice.Core.Domain.User;
// using Park20.Backoffice.Core.Dtos.Requests;
// using Park20.Backoffice.Core.IRepositories;
// using Park20.Backoffice.Core.IServices;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Park20.Backoffice.Tests.Application
// {
//     [TestFixture]
//     internal class UserServiceTests
//     {
//         [Test]
//         public async Task AddCustomer_Should_ReturnCreateCustomerResultDto()
//         {
//             // Arrange
//             var mockUserRepository = new Mock<IUserRepository>();
//             var mockParkyWalletService = new Mock<IParkyWalletService>();
//             var userService = new UserService(mockUserRepository.Object, mockParkyWalletService.Object);

//             var createCustomerRequest = new CreateCustomerRequestDto("Test User", "Password123", "test@example.com", "testuser");

//             var expectedCustomer = new Customer(createCustomerRequest.Username, createCustomerRequest.Email, createCustomerRequest.Password, createCustomerRequest.Name, false, null);

//             // Mock the behavior of AddCustomer in userRepository
//             mockUserRepository.Setup(repo => repo.AddCustomer(It.IsAny<Customer>())).ReturnsAsync(expectedCustomer);
//             mockParkyWalletService.Setup(s => s.Create()).ReturnsAsync(new ParkyWallet());

//             // Act
//             var result = await userService.AddCustomer(createCustomerRequest);

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(createCustomerRequest.Name, result.Name);
//             Assert.AreEqual(createCustomerRequest.Email, result.Email);
//             Assert.AreEqual(createCustomerRequest.Username, result.Username);
//         }
//     }
// }
