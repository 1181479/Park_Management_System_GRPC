using PaymentSimulation.Models;
using static PaymentSimulation.Services.PaymentService;

namespace PaymentSimulation.Services
{
    public interface IPaymentService
    {
        ProcessPaymentResult ProcessPayment(PaymentRequest paymentRequest);//, out string receipt, out string confirmation);
        bool AddToken(string token);
        TokenResponse GenerateToken(GenerateTokenRequest request);
    }
}
