using Park20.Backoffice.Core.Domain.Park;

namespace Park20.Backoffice.Core.IServices
{
    public interface IParkService
    {
        Task<bool?> GetVehicleTypeAvailable(string VehicleType, string ParkName);
        Task<bool?> GetAvailableSpace(string VehicleType, string ParkName);
        IEnumerable<Park> GetAllParks();
        Task<IEnumerable<Park>> GetAllParksWithDistance();
        Task<List<ParkingSpot>> GetNumberParkingSpots(string parkName);
        Task<bool?> UpdatePriceTable(Park priceTableDto);
        Task<List<string>> GetParkNames();
        Task<List<ParkingSpot>> GetParkingSpots(string parkName);
        Task<bool> UpdateParkingSpotStatus(bool status, int parkingSpotId);
        Task<Park> GetPriceTableByParkName(string parkName);

        public double CalculateDistanceBetweenCoordinates(double lat1, double lon1, double lat2, double lon2);

    }
}
