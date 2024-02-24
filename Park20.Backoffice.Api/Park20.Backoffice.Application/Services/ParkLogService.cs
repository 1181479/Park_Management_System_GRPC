using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Application.Services
{
    public class ParkLogService(IParkLogRepository parkLogRepository) : IParkLogService
    {
        private readonly IParkLogRepository _parkLogRepository = parkLogRepository;

        public async Task StartingCountingTimeOperation(ParkLog park)
        {
            var startingTime = DateTime.UtcNow;

            await CreateParkLog(park, startingTime);
        }

        private async Task CreateParkLog(ParkLog park, DateTime startingTime)
        {
            await _parkLogRepository.CreateParkLog(park, startingTime);
        }

        public async Task StopCountingTimeOperation(ParkLog park)
        {
            var endingTime = DateTime.UtcNow;
            await ManageParkLog(park, endingTime);
        }

        private async Task ManageParkLog(ParkLog park, DateTime endingTime)
        {
            park.EndTime = endingTime;

            var isUpdated = await _parkLogRepository.UpdateParkLogWithEndingTime(park);

        }

        public async Task<bool> UpdateAvailableParkingSpots(string parkName, string licensePlate, bool isEntrance)
        {
            return await _parkLogRepository.UpdateAvailableParkingSpots(parkName, licensePlate, isEntrance);
        }

    }
}
