using Park20.Backoffice.Core.Domain.ParkyWallets;

namespace Park20.Backoffice.Core.IServices
{
    public interface IParkyWalletService
    {
        public Task<bool> BulkParky(List<string> customerUsernames);
        public Task<List<int>> GetParkyValues();
        public Task<int> GetParkingValueAsync();
        public Task<int> GetRegestryValue();
        public Task<int> GetBulkValue();
        Task<ParkyWallet> GetParkyWalletByUsername(string username);
        public Task<bool> UpdateBulkValue(int value);
        public Task<bool> UpdateNewCustomerValue(int value);
        public Task<bool> UpdateParkingValue(int value);
        public Task<ParkyWallet> Create();
    }
}
