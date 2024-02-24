using Park20.Backoffice.Core.Domain.User;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IUserRepository
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> GetCustomer(string email);
        Task<List<Customer>> AllCustomer();
        Task<bool> CheckIfUserIsRegistered(string username, string password);
        Task<Customer> GetUserByUsername(string username);
        Task<bool> CheckIfEmailExists(string email);
        Task<Customer> GetCustomerByLicensePlate(string licensePlate);
    }
}
