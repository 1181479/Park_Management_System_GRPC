using Park20.Backoffice.Core.Domain.User;

namespace Park20.Backoffice.Core.IServices
{
    public interface IUserService
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<bool> CheckIfUserIsRegistered(string username, string password);
        Task<Customer>GetUserByUsername(string username);
        Task<List<Customer>>GetUsersBeforeDate(DateTime time);
        Task<bool> CheckIfEmailExists(string email);
    }
}
