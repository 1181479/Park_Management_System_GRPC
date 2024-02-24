using Grpc.Core;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Core.IServices;
using Proto;

namespace Park20.Backoffice.Api.Grpc
{
    public class ParkyWalletGrpcService : ParkyGrpc.ParkyGrpcBase
    {
        private readonly IParkyWalletService _parkyWalletService;

        public ParkyWalletGrpcService(IParkyWalletService parkyWalletService)
        {
            _parkyWalletService = parkyWalletService;
        }

        public override Task<UpdateResult> BulkParky(UsernameList request, ServerCallContext context)
        {
            return Task.FromResult(new UpdateResult
            {
                IsSuccessful = _parkyWalletService.BulkParky(request.Usernames.ToList().Select(u => u.Username).ToList()).Result
            });
        }

        public override Task<UpdateResult> UpdateBulkValue(AmountChangeRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UpdateResult
            {
                IsSuccessful = _parkyWalletService.UpdateBulkValue(request.Amount).Result
            });
        }

        public override Task<UpdateResult> UpdateNewCustomerValue(AmountChangeRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UpdateResult
            {
                IsSuccessful = _parkyWalletService.UpdateNewCustomerValue(request.Amount).Result
            });
        }

        public override Task<UpdateResult> UpdateParkingValue(AmountChangeRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UpdateResult
            {
                IsSuccessful = _parkyWalletService.UpdateParkingValue(request.Amount).Result
            });
        }

        public override Task<AmountChangeResult> GetParkingPerHourValue(EmptyMessage request, ServerCallContext context)
        {
            return Task.FromResult(new AmountChangeResult { Amount = _parkyWalletService.GetParkingValueAsync().Result });
        }

        public override Task<AmountChangeResult> GetRegistryValue(EmptyMessage request, ServerCallContext context)
        {
            return Task.FromResult(new AmountChangeResult { Amount = _parkyWalletService.GetRegestryValue().Result });
        }

        public override Task<AmountChangeResult> GetBulkValue(EmptyMessage request, ServerCallContext context)
        {
            return Task.FromResult(new AmountChangeResult { Amount = _parkyWalletService.GetBulkValue().Result });
        }

        public override Task<ParkyWallet> GetParkyWalletByUsername(GetUserRequest request, ServerCallContext context)
        {
            ParkyWallet pw = Mapper.Map(_parkyWalletService.GetParkyWalletByUsername(request.Username).Result);
            ParkyWallet filteredPW = new();
            request.FieldMask.Merge(pw, filteredPW);
            return Task.FromResult(filteredPW);
        }
    }
}
