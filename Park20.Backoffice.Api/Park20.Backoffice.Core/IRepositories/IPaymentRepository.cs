using Park20.Backoffice.Core.Domain.User;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IPaymentRepository
    {
        Task<string?> GetTokenFromLicensePlate(string licensePlate);
        Task<PaymentMethod?> AddPaymentMethod(PaymentMethod payment, string username);
        Task<IEnumerable<PaymentMethod>> GetAllFromUser(string username);
      
    }

}
