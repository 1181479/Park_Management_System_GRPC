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
//     public class PaymentRepositoryTests
//     {
//         [Test]
//         public async Task GetTokenFromLicensePlate()
//         {
//             // Arrange
//             var repo = new Mock<IPaymentRepository>();
//             string guid = Guid.NewGuid().ToString();
//             repo.Setup(r => r.GetTokenFromLicensePlate(It.IsAny<string>())).ReturnsAsync(guid);
//             // Act
//             var result = await repo.Object.GetTokenFromLicensePlate("cc-00-22");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(guid, result);
//         }

//         [Test]
//         public async Task AddPaymentMethod()
//         {
//             // Arrange
//             var repo = new Mock<IPaymentRepository>();
//             PaymentMethod paymentMethod = new PaymentMethod();
//             paymentMethod.FullName = "John Doe";
//             repo.Setup(r => r.AddPaymentMethod(It.IsAny<PaymentMethod>(), It.IsAny<string>())).ReturnsAsync(paymentMethod);
//             // Act
//             var result = await repo.Object.AddPaymentMethod(new PaymentMethod(), "username");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("John Doe", result.FullName);
//         }

//         [Test]
//         public async Task GetAllFromUser()
//         {
//             // Arrange
//             var repo = new Mock<IPaymentRepository>();
//             PaymentMethod paymentMethod = new PaymentMethod();
//             paymentMethod.FullName = "John Doe";
//             repo.Setup(r => r.GetAllFromUser(It.IsAny<string>())).ReturnsAsync(new List<PaymentMethod> { paymentMethod });
//             // Act
//             var result = await repo.Object.GetAllFromUser("username");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("John Doe", result.ToArray()[0].FullName);
//         }
//     }
// }