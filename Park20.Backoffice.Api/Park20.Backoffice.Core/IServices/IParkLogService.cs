using Park20.Backoffice.Core.Domain;

namespace Park20.Backoffice.Core.IServices
{
    public interface IParkLogService
    {
        Task StartingCountingTimeOperation(ParkLog park);
        Task StopCountingTimeOperation(ParkLog park);
        Task<bool> UpdateAvailableParkingSpots(string parkName, string licensePlate, bool isEntrance);
    }
}
