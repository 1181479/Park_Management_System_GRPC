using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;

namespace Park20.Backoffice.Core.IServices
{
    public interface IParkService
    {
        Task<bool?> GetVehicleTypeAvailable(string VehicleType, string ParkName);
        Task<bool?> GetAvailableSpace(string VehicleType, string ParkName);
        IEnumerable<Park> GetAllParks();
        Task<IEnumerable<ParkDistanceResultDto>> GetAllParksWithDistance(double targetLatitude, double targetLongitude);
        Task<ParkingSpotCountDto> GetNumberParkingSpots(string parkName);
        Task<bool?> UpdatePriceTable(PriceTableDto priceTableDto);
        Task<List<string>> GetParkNames();
        Task<List<ParkingSpot>> GetParkingSpots(string parkName);
        Task<bool> UpdateParkingSpotStatus(bool status, int parkingSpotId);
        Task<GetPriceTableDto> GetPriceTableByParkName(string parkName);

    }
}
