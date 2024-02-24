using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Application.Services
{
    public class DashboardService(IDashboardRepository dashboardRepository) : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository = dashboardRepository;

        public async Task<List<DashboardElements>> GetUsersWithLessParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes)
        {
            return await _dashboardRepository.GetUsersWithLessParkyCoinsUsage(parkName, initialDate, endDate, vehicleType, totalMinutes);
        }

        public async Task<List<DashboardElements>> GetUsersWithMidParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes)
        {
            return await _dashboardRepository.GetUsersWithMidParkyCoinsUsage(parkName, initialDate, endDate, vehicleType, totalMinutes);
        }

        public async Task<List<DashboardElements>> GetUsersWithMostParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes)
        {

            return await _dashboardRepository.GetUsersWithMostParkyCoinsUsage(parkName, initialDate, endDate, vehicleType, totalMinutes);
        }
    }
}
