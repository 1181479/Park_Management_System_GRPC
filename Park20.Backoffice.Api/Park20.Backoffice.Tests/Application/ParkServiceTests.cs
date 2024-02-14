using NUnit.Framework;
using Moq;
using Park20.Backoffice.Application.Mappers;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;
using Park20.Backoffice.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using global::Park20.Backoffice.Application.Services;
using global::Park20.Backoffice.Core.Domain.Park;
using global::Park20.Backoffice.Core.Domain;
using global::Park20.Backoffice.Core.IRepositories;

namespace Park20.Backoffice.Tests
{
    [TestFixture]
    public class ParkServiceTests
    {
        [Test]
        public async Task GetAvailableSpace_Should_ReturnFalse_WhenNoParkingSpots()
        {
            // Arrange
            var mockRepository = new Mock<IParkRepository>();
            mockRepository.Setup(repo => repo.GetParkingSpotsByParkName(It.IsAny<string>())).ReturnsAsync(new List<ParkingSpot>());
            var parkService = new ParkService(mockRepository.Object);
            var vehicleType = "Motocycle";
            var parkName = "ParkTest";

            // Act
            var result = await parkService.GetAvailableSpace(vehicleType, parkName);

            // Assert
            Assert.IsFalse(result);
        }


        [Test]
        public async Task GetAvailableSpace_Should_ReturnTrue_WhenAvailableSpaceExists()
        {
            // Arrange
            var mockRepository = new Mock<IParkRepository>();
            mockRepository.Setup(repo => repo.GetParkingSpotsByParkName(It.IsAny<string>())).ReturnsAsync(new List<ParkingSpot>
            {
                new ParkingSpot { VehicleType = VehicleType.Motocycle, Status = false }
            });

            var parkService = new ParkService(mockRepository.Object);
            var vehicleType = "Motocycle";
            var parkName = "ParkTest";

            // Act
            var result = await parkService.GetAvailableSpace(vehicleType, parkName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetVehicleTypeAvailable_Should_ReturnFalse_WhenNoParkingSpots()
        {
            // Arrange
            var mockRepository = new Mock<IParkRepository>();
            mockRepository.Setup(repo => repo.GetParkingSpotsByParkName(It.IsAny<string>())).ReturnsAsync(new List<ParkingSpot>());
            var parkService = new ParkService(mockRepository.Object);
            var vehicleType = "Motocycle";
            var parkName = "ParkTest";

            // Act
            var result = await parkService.GetVehicleTypeAvailable(vehicleType, parkName);

            // Assert
            Assert.IsFalse(result);
        }


        [Test]
        public async Task GetVehicleTypeAvailable_Should_ReturnTrue_WhenAvailableSpaceExists()
        {
            // Arrange
            var mockRepository = new Mock<IParkRepository>();
            mockRepository.Setup(repo => repo.GetParkingSpotsByParkName(It.IsAny<string>())).ReturnsAsync(new List<ParkingSpot>
            {
                new ParkingSpot { VehicleType = VehicleType.Motocycle, Status = false }
            });

            var parkService = new ParkService(mockRepository.Object);
            var vehicleType = "Motocycle";
            var parkName = "ParkTest";

            // Act
            var result = await parkService.GetVehicleTypeAvailable(vehicleType, parkName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllParks_Should_ReturnAllParksFromRepository()
        {
            // Arrange
            var mockRepository = new Mock<IParkRepository>();
            var expectedParks = new List<Park> { new Park { ParkName = "Park1" }, new Park { ParkName = "Park2" } };
            mockRepository.Setup(repo => repo.GetAllParks()).ReturnsAsync(expectedParks);
            var parkService = new ParkService(mockRepository.Object);

            // Act
            var result = parkService.GetAllParks();

            // Assert
            Assert.AreEqual(expectedParks, result);
        }

        [Test]
        public async Task GetAllParksWithDistance_Should_ReturnParksWithDistance()
        {
            // Arrange
            var mockRepository = new Mock<IParkRepository>();
            var parks = new List<Park> { new Park { ParkName = "Park1", Latitude = 10, Longitude = 20 } };
            mockRepository.Setup(repo => repo.GetAllParks()).ReturnsAsync(parks);
            var parkService = new ParkService(mockRepository.Object);
            var targetLatitude = 30;
            var targetLongitude = 40;

            // Act
            var result = await parkService.GetAllParksWithDistance(targetLatitude, targetLongitude);

            // Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(parks.Count, result.Count());
        }

        [Test]
        public async Task GetNumberParkingSpots_Should_ReturnParkingSpotCount()
        {
            // Arrange
            var mockRepository = new Mock<IParkRepository>();
            var parkName = "ParkTest";
            var parkingSpots = new List<ParkingSpot>
            {
                new ParkingSpot { VehicleType = VehicleType.Motocycle },
                new ParkingSpot { VehicleType = VehicleType.GPL },
                new ParkingSpot { VehicleType = VehicleType.Electric },
                new ParkingSpot { VehicleType = VehicleType.Automobile }
            };
            mockRepository.Setup(repo => repo.GetParkingSpotsAvailableByParkName(parkName)).ReturnsAsync(parkingSpots);
            var parkService = new ParkService(mockRepository.Object);

            // Act
            var result = await parkService.GetNumberParkingSpots(parkName);

            // Assert
            Assert.AreEqual(1, result.MotocycleCount);
            Assert.AreEqual(1, result.GPLCount);
            Assert.AreEqual(1, result.ElectricCount);
            Assert.AreEqual(1, result.AutomobileCount);
        }

        [Test]
        public async Task UpdatePriceTable()
        {
            // Arrange
            string parkName = "YourParkName";
            double nightFee = 10.99;
            DateTime initialDate = DateTime.Now;
            List<LinePriceTableDto> linePrices = new List<LinePriceTableDto>
            {
                new LinePriceTableDto(new PeriodDto("08:00:00", "12:00:00", new List<FractionsDto>
                {
                    new FractionsDto(1, 15, VehicleType.Motocycle, 2),
                    new FractionsDto(2, 30, VehicleType.Automobile, 1)
                }))
            };
            var updatePriceTableDto = new PriceTableDto(parkName, nightFee, initialDate, linePrices);

            var mockRepository = new Mock<IParkRepository>();
            mockRepository.Setup(repo => repo.UpdateParkPriceTable(parkName, nightFee, ParkMapper.ToPriceTableDomain(updatePriceTableDto))).ReturnsAsync(true);
            var parkService = new ParkService(mockRepository.Object);

            // Act
            var result = await parkService.UpdatePriceTable(updatePriceTableDto);

            // Assert
            Assert.False(result);
        }

    }
}
