using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;


namespace Park20.Backoffice.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;


        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle?> AddVehicleToUser(Vehicle vehicle, string username)
        {
            var result = await _vehicleRepository.AddVehicle(vehicle, username);
            if (result != null)
            {
                return result;
            }
            return default;
        }

        public async Task<Vehicle?> GetVehicle(string licence)
        {
            var result = await _vehicleRepository.GetVehicle(licence);
            if (result != null)
            {
                return result;
            }
            return default;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleListFromUser(string username)
        {
            return await _vehicleRepository.GetAllFromUser(username);
        }
    }
}
