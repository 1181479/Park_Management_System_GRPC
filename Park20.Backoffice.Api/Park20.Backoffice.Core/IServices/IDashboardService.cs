using Park20.Backoffice.Core.Domain;

namespace Park20.Backoffice.Core.IServices
{
    public interface IDashboardService
    {
        Task<List<DashboardElements>> GetUsersWithLessParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
        Task<List<DashboardElements>> GetUsersWithMidParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
        Task<List<DashboardElements>> GetUsersWithMostParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
    }
}
