using Park20.Backoffice.Core.Domain.ParkyWallets;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Application.Services
{
    public class ParkyWalletService : IParkyWalletService
    {
        private readonly IParkyCoinsConfigurationRepository _parkyCoinsConfigurationRepository;
        private readonly IParkyWalletRepository _parkyWalletRepository;
        private readonly IUserRepository _userRepository;


        public ParkyWalletService(IParkyCoinsConfigurationRepository parkyCoinsConfigurationRepository, IParkyWalletRepository parkyWalletRepository, IUserRepository userRepository)
        {
            _parkyCoinsConfigurationRepository = parkyCoinsConfigurationRepository;
            _parkyWalletRepository = parkyWalletRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> BulkParky(List<string> customerUsernames)
        {
            List<Customer> customers = [];
            foreach (var customerUsername in customerUsernames)
            {
                customers.Add((await _userRepository.GetUserByUsername(customerUsername)));
            }
            return await _parkyWalletRepository.BulkAdd(customers.Select(u => u.ParkyWalletId).ToList(), await GetBulkValue());
        }

        public async Task<List<int>> GetParkyValues()
        {
            return [await GetBulkValue(), await GetRegestryValue(), await GetParkingValueAsync()];
        }

        public async Task<int> GetParkingValueAsync()
        {
            return await _parkyCoinsConfigurationRepository.GetParkingValue();
        }

        public async Task<int> GetRegestryValue()
        {
            return await _parkyCoinsConfigurationRepository.GetNewCustomerValue();
        }

        public async Task<int> GetBulkValue()
        {
            return await _parkyCoinsConfigurationRepository.GetBulkValue();
        }

        public async Task<ParkyWallet> GetParkyWalletByUsername(string username)
        {
            var result = await _parkyWalletRepository.GetParkyWalletByUsername(username);
            if(result == null)
            {
                return default;
            }
            return result;
        }
            

        public async Task<bool> UpdateBulkValue(int value)
        {
            await _parkyCoinsConfigurationRepository.UpdateBulkValue(value);
            return true;
        }

        public async Task<bool> UpdateNewCustomerValue(int value)
        {
            await _parkyCoinsConfigurationRepository.UpdateNewCustomerValue(value);
            return true;
        }

        public async Task<bool> UpdateParkingValue(int value)
        {
            await _parkyCoinsConfigurationRepository.UpdateCurrencyValue(value);
            return true;
        }

        public async Task<ParkyWallet> Create()
        {
            return await _parkyWalletRepository.Create(await GetRegestryValue());;
        }
    }
}
