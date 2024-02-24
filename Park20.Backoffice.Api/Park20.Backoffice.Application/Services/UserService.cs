using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Application.Services
{
    public class UserService(IUserRepository userRepository, IParkyWalletService parkyWalletService) : IUserService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IParkyWalletService parkyWalletService = parkyWalletService;

        public async Task<Customer> AddCustomer(Customer customer)
        {
            customer.ParkyWalletId = (await parkyWalletService.Create()).Id;
            return await userRepository.AddCustomer(customer);
        }

        public async Task<bool> CheckIfUserIsRegistered(string username, string password)
        {
            return await userRepository.CheckIfUserIsRegistered(username, password);
        }

        public async Task<Customer> GetUserByUsername(string username)
        {
            return await userRepository.GetUserByUsername(username);
        }

        public async Task<List<Customer>> GetUsersBeforeDate(DateTime time)
        {
            List<Customer> customers = await userRepository.AllCustomer();
            return customers.FindAll(u => u.RegistrationDate < time);
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            return await userRepository.CheckIfEmailExists(email);
        }
    }
}
