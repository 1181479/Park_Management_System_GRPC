using Park20.Backoffice.Core.Domain;

namespace Park20.Backoffice.Core.IServices
{
    public interface IVehicleService
    {

        Task<Vehicle?> AddVehicleToUser(Vehicle vehicle, string username);
        Task<Vehicle?> GetVehicle(string licence);
        Task<IEnumerable<Vehicle>> GetVehicleListFromUser(string username);
    }
}
