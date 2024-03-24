
using Google.Protobuf;
using PaymentSimulation.Models;
using Protos;

namespace PaymentSimulation.ProtoMap
{
    public static class Mapper
    {
        public static Models.PaymentRequest Map(Protos.PaymentRequest paymentRequest)
        {
            return new Models.PaymentRequest
            {
                Token = paymentRequest.Token,
                Amount = decimal.Parse(paymentRequest.Amount.ToString())
            };
        }

        public static Models.GenerateTokenRequest Map(Protos.GenerateTokenRequest tokenReq)
        {
            return new Models.GenerateTokenRequest(long.Parse(tokenReq.CardNumber.ToString()),
                tokenReq.Cvv,
                tokenReq.FullName,
                tokenReq.ExpirationDate.ToDateTime()
            );
        }

        public static GenerateTokenResponse Map(TokenResponse tokenResponse)
        {
            return new GenerateTokenResponse
            {
                Token = tokenResponse.Token
            };
        }

        public static AddTokenResponse Map(bool v)
        {
            return new AddTokenResponse
            {
                Result = v
            };
        }
        
        public static PaymentResponse MapPaymentReponse(bool v)
        {
            return new PaymentResponse
            {
                Result = v
            };
        }
    }
}
