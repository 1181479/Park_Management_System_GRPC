using PaymentSimulation.Models;
using System.Dynamic;
using System.Text;

namespace PaymentSimulation.Services
{
    public class PaymentService : IPaymentService
    {

        static readonly List<string> tokens = [];
        public bool ProcessPayment(PaymentRequest paymentRequest)//, out string receipt, out string confirmation)
        {
            bool token = FindToken(paymentRequest.Token);
            bool paymentSuccessful = SimulatePaymentProcessing(paymentRequest, token);

            if (paymentSuccessful)
            {
                //receipt = GeneratePaymentReceipt(paymentRequest);
                //confirmation = GenerateConfirmation(paymentRequest, user);
                Console.WriteLine("Payment Successful");
                return true;
            }
            else
            {
                //receipt = null;
                //confirmation = null;
                Console.WriteLine("Payment Failed");
                return false;
            }
        }

        public TokenResponse GenerateToken(GenerateTokenRequest request)
        {
            DateTime currentDate = DateTime.Now;
            TokenResponse resultToken = new("");
            if (request.ExpirationDate.Date < currentDate.Date)
            {
                return resultToken;
            }
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int index = random.Next(characters.Length);
                result.Append(characters[index]);
            }
            TokenResponse resultCorrectToken = new(result.ToString());
            return resultCorrectToken;
        }

        public bool AddToken(string token)
        {
            tokens.Add(token);
            return true;
        }

        private bool FindToken(string token)
        {
            if (token != null)
                return tokens.Any(t => t == token);
            return false;
        }

        private bool SimulatePaymentProcessing(PaymentRequest paymentRequest, bool token)
        {
            Console.WriteLine("Processing...");
            Thread.Sleep(1000);
            return token;
        }

        private string GeneratePaymentReceipt(PaymentRequest paymentRequest)
        {
            return $"Payment Receipt\nAmount: ${paymentRequest.Amount}\nDate: {DateTime.Now}";
        }

        private string GenerateConfirmation(PaymentRequest paymentRequest, string token)
        {
            return $"Payment of ${paymentRequest.Amount} confirmed for token: {token}";
        }
    }
}
