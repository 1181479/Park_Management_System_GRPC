using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IParkyCoinsConfigurationRepository
    {
        Task<int> GetBulkValue();
        Task<int> GetNewCustomerValue();
        Task<int> GetParkingValue();
        Task<int> GetCurrencyValue();
        Task<bool> UpdateBulkValue(int value);
        Task<bool> UpdateNewCustomerValue(int value);
        Task<bool> UpdateCurrencyValue(int value);
        Task<bool> UpdateParkingValue(int value);
        
    }
}
