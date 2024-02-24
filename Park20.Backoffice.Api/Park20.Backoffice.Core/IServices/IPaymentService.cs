using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.User;

namespace Park20.Backoffice.Core.IServices
{
    public interface IPaymentService
    {
        Task<PaymentMethod?> AddPaymentMethodToUser(PaymentMethod paymentMethod, string username);
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodListFromUser(string username);
        Task<HibridPayment> MakePayment(string licensePlate, decimal totalCost);
        Task<decimal> CalculateCost(string parkName, string licensePlate);
    }
}
