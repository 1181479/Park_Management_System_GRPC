using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Application.Services
{
    public class ParkService : IParkService
    {
        private readonly IParkRepository _parkRepository;

        private const double EarthRadiusKm = 6371.0;

        public ParkService(IParkRepository parkRepository)
        {
            _parkRepository = parkRepository;
        }

        public async Task<bool?> GetAvailableSpace(string vehicleType, string parkName)
        {
            List<ParkingSpot> parkingSpots = await _parkRepository.GetParkingSpotsByParkName(parkName);
            if (parkingSpots.Count == 0)
            {
                return false;
            }
            return parkingSpots.FindAll(spot => spot.VehicleType == Enum.Parse<VehicleType>(vehicleType) && spot.Status == false).Count > 0;
        }

        public async Task<bool?> GetVehicleTypeAvailable(string vehicleType, string parkName)
        {
            List<ParkingSpot> parkingSpots = await _parkRepository.GetParkingSpotsByParkName(parkName);
            if (parkingSpots.Count == 0)
            {
                return false;
            }
            return parkingSpots.Any(spot => spot.VehicleType == Enum.Parse<VehicleType>(vehicleType) && spot.Status == false);
        }

        public IEnumerable<Park> GetAllParks()
        {
            var result = _parkRepository.GetAllParks().Result;
            return result;
        }

        public async Task<IEnumerable<Park>> GetAllParksWithDistance()
        {
            return await _parkRepository.GetAllParks();
        }

        public async Task<List<ParkingSpot>> GetNumberParkingSpots(string parkName)
        {
            return await _parkRepository.GetParkingSpotsAvailableByParkName(parkName);
        }


        public double CalculateDistanceBetweenCoordinates(double lat1, double lon1, double lat2, double lon2)
        {

            // Converte as coordenadas para radianos
            lat1 = ToRadians(lat1);
            lon1 = ToRadians(lon1);
            lat2 = ToRadians(lat2);
            lon2 = ToRadians(lon2);

            // Diferença nas latitudes e longitudes
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            // Fórmula de Haversine
            double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Distância em quilômetros
            double distance = EarthRadiusKm * c;

            return distance;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public async Task<bool?> UpdatePriceTable(Park park)
        {
            return await _parkRepository.UpdateParkPriceTable(park.ParkName, park.NightFee, park.PriceTable);
        }

        public async Task<List<string>> GetParkNames()
        {
            return await this._parkRepository.GetParkNames();
        }

        public async Task<List<ParkingSpot>> GetParkingSpots(string parkName)
        {
            return await this._parkRepository.GetParkingSpotsByParkName(parkName);
        }

        public async Task<bool> UpdateParkingSpotStatus(bool status, int parkingSpotId)
        {
            return await this._parkRepository.UpdateParkingSpotStatus(status, parkingSpotId);
        }

        public async Task<Park> GetPriceTableByParkName(string parkName)
        {
            return await _parkRepository.GetParkByName(parkName);
        }
    }
}
