using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using PaymentSimulation.Models;
using PaymentSimulation.ProtoMap;
using PaymentSimulation.Services;
using Protos;

namespace PaymentSimulation.Grpc
{
    public class PaymentGrpcService : PaymentGrpc.PaymentGrpcBase
    {

        private readonly IPaymentService _paymentService;

        public PaymentGrpcService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public override Task<AddTokenResponse> AddToken(AddTokenRequest request, ServerCallContext context)
        {
            AddTokenResponse response = new();
            request.FieldMask.Merge(Mapper.Map(_paymentService.AddToken(request.Token)), response);
            return Task.FromResult(response);
        }

        public override Task<GenerateTokenResponse> GenerateToken(Protos.GenerateTokenRequest request, ServerCallContext context)
        {
            GenerateTokenResponse response = new();
            request.FieldMask.Merge(Mapper.Map(_paymentService.GenerateToken(Mapper.Map(request))), response);
            return Task.FromResult(response);
        }

        public override Task<PaymentResponse> ProcessPayment(Protos.PaymentRequest request, ServerCallContext context)
        {
            PaymentResponse response = new();
            request.FieldMask.Merge(Mapper.MapPaymentReponse(_paymentService.ProcessPayment(Mapper.Map(request))), response);
            return Task.FromResult(response);
        }
    }
}
