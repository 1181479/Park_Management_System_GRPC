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
//     public class CustomerRepositoryTests
//     {
//         [Test]
//         public async Task AddCustomer()
//         {
//             // Arrange
//             var repo = new Mock<IUserRepository>();
//             Customer customer = new Customer("username", "email", "password", "name", false, null);
//             repo.Setup(r => r.AddCustomer(It.IsAny<Customer>())).ReturnsAsync(customer);
//             // Act
//             var result = await repo.Object.AddCustomer(new Customer());

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("name", result.Name);
//         }

//         [Test]
//         public async Task GetCustomer()
//         {
//             // Arrange
//             var repo = new Mock<IUserRepository>();
//             Customer customer = new Customer("username", "email", "password", "name", false, null);
//             repo.Setup(r => r.GetCustomer(It.IsAny<string>())).ReturnsAsync(customer);
//             // Act
//             var result = await repo.Object.GetCustomer("email");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("name", result.Name);
//         }
//     }
// }