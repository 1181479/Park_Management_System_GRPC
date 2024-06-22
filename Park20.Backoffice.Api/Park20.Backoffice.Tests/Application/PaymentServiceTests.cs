// using NUnit.Framework;
// using Moq;
// using Park20.Backoffice.Core.Domain;
// using Park20.Backoffice.Core.Dtos.Requests;
// using Park20.Backoffice.Core.Dtos.Results;
// using Park20.Backoffice.Core.IRepositories;
// using Park20.Backoffice.Application.Services;
// using Microsoft.Extensions.Configuration;
// using Park20.Backoffice.Core.Domain.User;
// using Park20.Backoffice.Core.Domain.Park;


// namespace Park20.Backoffice.Tests
// {
//     [TestFixture]
//     public class PaymentServiceTests
//     {
//         [Test]
//         public async Task MakePayment_Should_ReturnFalse_WhenTokenIsNullOrWhiteSpace()
//         {
//             // Arrange
//             var mockPaymentRepository = new Mock<IPaymentRepository>();
//             var mockInvoiceRepository = new Mock<IInvoiceRepository>();
//             var mockParkRepository = new Mock<IParkRepository>();
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var mockParkLogRepository = new Mock<IParkLogRepository>();
//             var mockParkyWalletRepository = new Mock<IParkyWalletRepository>();
//             var mockUserRepository = new Mock<IUserRepository>();
//             var mockParkyCoinsConfigurationRepository = new Mock<IParkyCoinsConfigurationRepository>();
//             var mockConfiguration = new Mock<IConfiguration>();

//             var paymentService = new PaymentService(
//                 mockPaymentRepository.Object,
//                 mockInvoiceRepository.Object,
//                 mockConfiguration.Object,
//                 mockParkRepository.Object,
//                 mockVehicleRepository.Object,
//                 mockParkLogRepository.Object,
//                 mockParkyWalletRepository.Object,
//                 mockUserRepository.Object,
//                 mockParkyCoinsConfigurationRepository.Object
//                 );


//             var licensePlate = "ABC123";
//             var totalCost = 100.0m;

//             // Act
//             var result = await paymentService.MakePayment(licensePlate, totalCost);

//             // Assert
//             Assert.IsTrue(result.isSuccessfull);
//         }



//         [Test]
//         public async Task GetPaymentMethodListFromUser_Should_ReturnListOfPaymentMethods()
//         {
//             // Arrange
//             var mockPaymentRepository = new Mock<IPaymentRepository>();
//             var mockInvoiceRepository = new Mock<IInvoiceRepository>();
//             var mockParkRepository = new Mock<IParkRepository>();
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var mockParkLogRepository = new Mock<IParkLogRepository>();
//             var mockParkyWalletRepository = new Mock<IParkyWalletRepository>();
//             var mockUserRepository = new Mock<IUserRepository>();
//             var mockParkyCoinsConfigurationRepository = new Mock<IParkyCoinsConfigurationRepository>();
//             var mockConfiguration = new Mock<IConfiguration>();

//             var paymentService = new PaymentService(
//                 mockPaymentRepository.Object,
//                 mockInvoiceRepository.Object,
//                 mockConfiguration.Object,
//                 mockParkRepository.Object,
//                 mockVehicleRepository.Object,
//                 mockParkLogRepository.Object,
//                 mockParkyWalletRepository.Object,
//                 mockUserRepository.Object,
//                 mockParkyCoinsConfigurationRepository.Object
//                 );

//             var username = "user123";

//             // Act
//             var result = await paymentService.GetPaymentMethodListFromUser(username);

//             // Assert
//             mockPaymentRepository.Verify(repo => repo.GetAllFromUser(username), Times.Once);
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<IEnumerable<PaymentMethodResultDto>>(result);
//         }

//         [Test]
//         public async Task AddPaymentMethodToUser_Should_ReturnPaymentMethodDto_WhenSuccessfullyAdded()
//         {
//             // Arrange
//             var mockPaymentRepository = new Mock<IPaymentRepository>();
//             mockPaymentRepository.Setup(repo => repo.AddPaymentMethod(It.IsAny<PaymentMethod>(), It.IsAny<string>()))
//                 .ReturnsAsync(new PaymentMethod { FullName = "John Doe", CardLastFourDigits = 1234, ExpirationDate = DateTime.Now.AddMonths(12), PaymentToken = "valid_token" });

//             var mockInvoiceRepository = new Mock<IInvoiceRepository>();
//             var mockParkRepository = new Mock<IParkRepository>();
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var mockParkLogRepository = new Mock<IParkLogRepository>();
//             var mockParkyWalletRepository = new Mock<IParkyWalletRepository>();
//             var mockUserRepository = new Mock<IUserRepository>();
//             var mockParkyCoinsConfigurationRepository = new Mock<IParkyCoinsConfigurationRepository>();
//             var mockConfiguration = new Mock<IConfiguration>();

//             var paymentService = new PaymentService(
//                 mockPaymentRepository.Object,
//                 mockInvoiceRepository.Object,
//                 mockConfiguration.Object,
//                 mockParkRepository.Object,
//                 mockVehicleRepository.Object,
//                 mockParkLogRepository.Object,
//                 mockParkyWalletRepository.Object,
//                 mockUserRepository.Object,
//                 mockParkyCoinsConfigurationRepository.Object);

//             var createPaymentMethodRequestDto = new CreatePaymentMethodRequestDto
//             (
//                 1234,
//                 DateTime.Now.AddMonths(12),
//                 "John Doe",
//                 "valid_token",
//                 "user123"
//             );

//             // Act
//             var result = await paymentService.AddPaymentMethodToUser(createPaymentMethodRequestDto);

//             // Assert
//             mockPaymentRepository.Verify(repo => repo.AddPaymentMethod(It.IsAny<PaymentMethod>(), createPaymentMethodRequestDto.Username), Times.Once);
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<PaymentMethodResultDto>(result);
//         }

//         [Test]
//         public async Task AddPaymentMethodToUser_Should_ReturnNull_WhenFailedToAdd()
//         {
//             // Arrange
//             var mockPaymentRepository = new Mock<IPaymentRepository>();
//             mockPaymentRepository.Setup(repo => repo.AddPaymentMethod(It.IsAny<PaymentMethod>(), It.IsAny<string>()))
//                 .ReturnsAsync((PaymentMethod)null);

//             var mockInvoiceRepository = new Mock<IInvoiceRepository>();
//             var mockParkRepository = new Mock<IParkRepository>();
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var mockParkLogRepository = new Mock<IParkLogRepository>();
//             var mockParkyWalletRepository = new Mock<IParkyWalletRepository>();
//             var mockUserRepository = new Mock<IUserRepository>();
//             var mockParkyCoinsConfigurationRepository = new Mock<IParkyCoinsConfigurationRepository>();
//             var mockConfiguration = new Mock<IConfiguration>();

//             var paymentService = new PaymentService(
//                 mockPaymentRepository.Object,
//                 mockInvoiceRepository.Object,
//                 mockConfiguration.Object,
//                 mockParkRepository.Object,
//                 mockVehicleRepository.Object,
//                 mockParkLogRepository.Object,
//                 mockParkyWalletRepository.Object,
//                 mockUserRepository.Object,
//                 mockParkyCoinsConfigurationRepository.Object);

//             var createPaymentMethodRequestDto = new CreatePaymentMethodRequestDto
//             (
//                  1234,
//                  DateTime.Now.AddMonths(12),
//                  "John Doe",
//                  "valid_token",
//                  "user123"
//             );

//             // Act
//             var result = await paymentService.AddPaymentMethodToUser(createPaymentMethodRequestDto);

//             // Assert
//             mockPaymentRepository.Verify(repo => repo.AddPaymentMethod(It.IsAny<PaymentMethod>(), createPaymentMethodRequestDto.Username), Times.Once);
//             Assert.IsNull(result);
//         }

//         [Test]
//         public async Task CalculateCost_Should_ReturnZero_WhenVehicleNotFound()
//         {
//             // Arrange
//             var mockParkLogRepository = new Mock<IParkLogRepository>();
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             mockVehicleRepository.Setup(repo => repo.GetVehicle(It.IsAny<string>()))
//                 .ReturnsAsync((Vehicle)null);

//             var mockParkRepository = new Mock<IParkRepository>();
//             var mockParkyWalletRepository = new Mock<IParkyWalletRepository>();
//             var mockUserRepository = new Mock<IUserRepository>();
//             var mockParkyCoinsConfigurationRepository = new Mock<IParkyCoinsConfigurationRepository>();
//             var mockConfiguration = new Mock<IConfiguration>();

//             var paymentService = new PaymentService(
//                 Mock.Of<IPaymentRepository>(),
//                 Mock.Of<IInvoiceRepository>(),
//                 mockConfiguration.Object,
//                 mockParkRepository.Object,
//                 mockVehicleRepository.Object,
//                 mockParkLogRepository.Object,
//                 mockParkyWalletRepository.Object,
//                 mockUserRepository.Object,
//                 mockParkyCoinsConfigurationRepository.Object);

//             // Act
//             var result = await paymentService.CalculateCost("ParkName", "InvalidLicensePlate");

//             // Assert
//             Assert.AreEqual(0, result);
//         }

//         // ?????????????????????
//         //[Test]
//         //public async Task CalculateCost_Should_ReturnCorrectCost()
//         //{
//         //    // Arrange
//         //    var mockParkLogRepository = new Mock<IParkLogRepository>();
//         //    var mockVehicleRepository = new Mock<IVehicleRepository>();
//         //    var mockParkRepository = new Mock<IParkRepository>();

//         //    // Mock the park log, vehicle, and park information
//         //    var parkLog = new ParkLog { StartTime = DateTime.Now.AddHours(-2), EndTime = DateTime.Now };
//         //    var vehicle = new Vehicle { Type = VehicleType.Automobile};
//         //    var park = new Park { ParkName = "ParkName", PriceTable = new PriceTable { LinePrices = new List<LinePriceTable>() } };

//         //    mockParkLogRepository.Setup(repo => repo.GetParkLog(It.IsAny<string>())).ReturnsAsync(parkLog);
//         //    mockVehicleRepository.Setup(repo => repo.GetVehicle(It.IsAny<string>())).ReturnsAsync(vehicle);
//         //    mockParkRepository.Setup(repo => repo.GetParkByName(It.IsAny<string>())).ReturnsAsync(park);

//         //    var paymentService = new PaymentService(
//         //        Mock.Of<IPaymentRepository>(),
//         //        Mock.Of<IInvoiceRepository>(),
//         //        Mock.Of<IConfiguration>(),
//         //        mockParkRepository.Object,
//         //        mockVehicleRepository.Object,
//         //        mockParkLogRepository.Object);

//         //    // Act
//         //    var result = await paymentService.CalculateCost("ParkName", "ValidLicensePlate");

//         //    Assert.AreEqual(0, result);


//         //}


//     }
// }
