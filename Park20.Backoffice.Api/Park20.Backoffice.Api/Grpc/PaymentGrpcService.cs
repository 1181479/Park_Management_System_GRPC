using Grpc.Core;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Core.Domain.User;
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
            PaymentMethodResult pmr = Mapper.Map(_paymentService.AddPaymentMethodToUser(Mapper.Map(request), request.Username).Result);
            PaymentMethodResult filteredPmr = new PaymentMethodResult();
            request.FieldMask.Merge(pmr, filteredPmr);
            return Task.FromResult(filteredPmr);
        }

        public override Task<ListPaymentMethodResult> GetPaymentMethodListFromUser(GetPaymentMethodListRequest request, ServerCallContext context)
        {
            ListPaymentMethodResult lpmr = Mapper.Map(_paymentService.GetPaymentMethodListFromUser(request.Username).Result);
            ListPaymentMethodResult lfilteredPmr = new();
            if (!request.FieldMask.ToString().Contains("listPaymentMethod"))
            {
                foreach (PaymentMethodResult pmr in lpmr.ListPaymentMethod)
                {
                    PaymentMethodResult filteredPmr = new();
                    request.FieldMask.Merge(pmr, filteredPmr);
                    lfilteredPmr.ListPaymentMethod.Add(filteredPmr);
                }
            }
            else
            {
                request.FieldMask.Merge(lpmr, lfilteredPmr);
            }
            return Task.FromResult(lfilteredPmr);
        }
    }
}
