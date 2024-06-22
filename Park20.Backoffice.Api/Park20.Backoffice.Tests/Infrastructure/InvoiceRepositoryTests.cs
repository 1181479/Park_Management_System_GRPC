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
// using Park20.Backoffice.Core.Domain.Payment;

// namespace Park20.Backoffice.Tests
// {
//     [TestFixture]
//     public class InvoiceRepositoryTests
//     {
//         [Test]
//         public async Task CreateInvoice()
//         {
//             // Arrange
//             var repo = new Mock<IInvoiceRepository>();
//             Invoice invoice = new Invoice(10, false);
//             repo.Setup(r => r.CreateInvoice(It.IsAny<Invoice>(), It.IsAny<string>())).ReturnsAsync(invoice);
//             // Act
//             var result = await repo.Object.CreateInvoice(new Invoice(10, false), "cc-00-22");

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(10, result.TotalCost);
//         }
//     }
// }