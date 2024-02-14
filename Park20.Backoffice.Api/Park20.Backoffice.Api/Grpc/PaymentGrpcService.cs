using Grpc.Core;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IServices;
using Proto;

namespace Park20.Backoffice.Api.Grpc
{
    public class PaymentGrpcService : PaymentGrpc.PaymentGrpcBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentGrpcService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public override Task<PaymentMethodResult> AddPaymentMethod(CreatePaymentMethodRequest request, ServerCallContext context)
        {
            PaymentMethodResultDto resultDto = _paymentService.AddPaymentMethodToUser(Mapper.Map(request)).Result;
            return Task.FromResult(new PaymentMethodResult { FullName = resultDto.FullName, LastFourDigits = resultDto.LastFourDigits });
        }

        public override Task<ListPaymentMethodResult> GetPaymentMethodListFromUser(GetPaymentMethodListRequest request, ServerCallContext context)
        {
            return Mapper.Map(_paymentService.GetPaymentMethodListFromUser(request.Username).Result);
        }
    }
}
