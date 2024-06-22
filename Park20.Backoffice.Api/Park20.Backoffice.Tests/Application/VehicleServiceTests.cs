// using Moq;
// using NUnit.Framework;
// using Park20.Backoffice.Application.Services;
// using Park20.Backoffice.Core.Domain;
// using Park20.Backoffice.Core.Dtos.Requests;
// using Park20.Backoffice.Core.IRepositories;

// namespace Park20.Backoffice.Tests.Application
// {
//     [TestFixture]
//     internal class VehicleServiceTests
//     {
//         [Test]
//         public async Task AddVehicleToUser_Should_ReturnVehicleResultDto_WhenSuccessfullyAdded()
//         {
//             // Arrange
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var vehicleService = new VehicleService(mockVehicleRepository.Object);

//             var createVehicleRequest = new CreateVehicleRequestDto ("AB-00-00", "Toyota", "Corolla", "Automobile", "testuser");

//             var expectedVehicle = new Vehicle
//             {
//                 LicensePlate = createVehicleRequest.LicensePlate,
//                 Brand = createVehicleRequest.Brand,
//                 Model = createVehicleRequest.Model,
//                 Type = Enum.Parse<VehicleType>(createVehicleRequest.Type)
//             };

//             // Set up the mock behavior for AddVehicle in vehicleRepository
//             mockVehicleRepository.Setup(repo => repo.AddVehicle(It.IsAny<Vehicle>(), It.IsAny<string>())).ReturnsAsync(expectedVehicle);

//             // Act
//             var result = await vehicleService.AddVehicleToUser(createVehicleRequest);

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(createVehicleRequest.LicensePlate, result.LicensePlate);
//             Assert.AreEqual(createVehicleRequest.Brand, result.Brand);
//             Assert.AreEqual(createVehicleRequest.Model, result.Model);
//         }

//         [Test]
//         public async Task GetVehicle_Should_ReturnVehicleResultDto_WhenVehicleExists()
//         {
//             // Arrange
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var vehicleService = new VehicleService(mockVehicleRepository.Object);

//             var licensePlate = "AB-00-00";

//             var expectedVehicle = new Vehicle
//             {
//                 LicensePlate = licensePlate,
//                 Brand = "Toyota",
//                 Model = "Corolla",
//                 Type = VehicleType.Automobile
//             };

//             // Set up the mock behavior for GetVehicle in vehicleRepository
//             mockVehicleRepository.Setup(repo => repo.GetVehicle(It.IsAny<string>())).ReturnsAsync(expectedVehicle);

//             // Act
//             var result = await vehicleService.GetVehicle(licensePlate);

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(expectedVehicle.LicensePlate, result.LicensePlate);
//             Assert.AreEqual(expectedVehicle.Brand, result.Brand);
//             Assert.AreEqual(expectedVehicle.Model, result.Model);
//         }

//         [Test]
//         public async Task GetVehicleListFromUser_Should_ReturnListOfVehicleResultDto()
//         {
//             // Arrange
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var vehicleService = new VehicleService(mockVehicleRepository.Object);

//             var username = "user123";

//             var expectedVehicleList = new List<Vehicle>
//             {
//                 new Vehicle
//                 {
//                     LicensePlate = "AB-00-00",
//                     Brand = "Toyota",
//                     Model = "Corolla",
//                     Type = VehicleType.Automobile
//                 },
//             };

//             // Set up the mock behavior for GetAllFromUser in vehicleRepository
//             mockVehicleRepository.Setup(repo => repo.GetAllFromUser(It.IsAny<string>())).ReturnsAsync(expectedVehicleList);

//             // Act
//             var result = await vehicleService.GetVehicleListFromUser(username);

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(expectedVehicleList.Count, result.Count());
//         }

//         [Test]
//         public async Task AddVehicleToUser_Should_ReturnNull_WhenRepositoryReturnsNull()
//         {
//             // Arrange
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var vehicleService = new VehicleService(mockVehicleRepository.Object);

//             var createVehicleRequest = new CreateVehicleRequestDto("AB-00-00", "Toyota", "Corolla", "Automobile", "testuser");
     

//             // Set up the mock behavior for AddVehicle in vehicleRepository to return null
//             mockVehicleRepository.Setup(repo => repo.AddVehicle(It.IsAny<Vehicle>(), It.IsAny<string>())).ReturnsAsync((Vehicle)null);

//             // Act
//             var result = await vehicleService.AddVehicleToUser(createVehicleRequest);

//             // Assert
//             Assert.IsNull(result);
//         }

//         [Test]
//         public async Task GetVehicle_Should_ReturnNull_WhenVehicleDoesNotExist()
//         {
//             // Arrange
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var vehicleService = new VehicleService(mockVehicleRepository.Object);

//             var licensePlate = "NonExistentPlate"; // Provide a non-existent license plate

//             // Set up the mock behavior for GetVehicle in vehicleRepository to return null
//             mockVehicleRepository.Setup(repo => repo.GetVehicle(It.IsAny<string>())).ReturnsAsync((Vehicle)null);

//             // Act
//             var result = await vehicleService.GetVehicle(licensePlate);

//             // Assert
//             Assert.IsNull(result);
//         }

//         [Test]
//         public async Task GetVehicleListFromUser_Should_ReturnEmptyList_WhenUserHasNoVehicles()
//         {
//             // Arrange
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var vehicleService = new VehicleService(mockVehicleRepository.Object);

//             var username = "user123"; // Provide a valid username

//             // Set up the mock behavior for GetAllFromUser in vehicleRepository to return an empty list
//             mockVehicleRepository.Setup(repo => repo.GetAllFromUser(It.IsAny<string>())).ReturnsAsync(new List<Vehicle>());

//             // Act
//             var result = await vehicleService.GetVehicleListFromUser(username);

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsEmpty(result);
//         }

//         [Test]
//         public async Task AddVehicleToUser_Should_ReturnVehicleResultDto_WhenSuccessfullyAdded_NewDomainClasses()
//         {
//             // Arrange
//             var mockVehicleRepository = new Mock<IVehicleRepository>();
//             var vehicleService = new VehicleService(mockVehicleRepository.Object);

//             var createVehicleRequest = new CreateVehicleRequestDto("AB-00-00","Toyota","Corolla", "Automobile", "testuser");

//             var expectedVehicle = new Vehicle
//             {
//                 LicensePlate = createVehicleRequest.LicensePlate,
//                 Brand = createVehicleRequest.Brand,
//                 Model = createVehicleRequest.Model,
//                 Type = Enum.Parse<VehicleType>(createVehicleRequest.Type)
//             };

//             // Set up the mock behavior for AddVehicle in vehicleRepository
//             mockVehicleRepository.Setup(repo => repo.AddVehicle(It.IsAny<Vehicle>(), It.IsAny<string>())).ReturnsAsync(expectedVehicle);

//             // Act
//             var result = await vehicleService.AddVehicleToUser(createVehicleRequest);

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(createVehicleRequest.LicensePlate, result.LicensePlate);
//             Assert.AreEqual(createVehicleRequest.Brand, result.Brand);
//             Assert.AreEqual(createVehicleRequest.Model, result.Model);
//         }
//     }
// }
