using Grpc.Core;
using Park20.Backoffice.Api.ProtoMap;
using Park20.Backoffice.Core.IServices;
using Proto;

namespace Park20.Backoffice.Api.Grpc
{
    public class UserGrpcService : UserGrpc.UserGrpcBase
    {
        private readonly IUserService _userService;

        public UserGrpcService(IUserService userService)
        {
            _userService = userService;
        }

        public override Task<CreateCustomerResult> AddCustomer(CreateCustomerRequest request, ServerCallContext context)
        {
            if (_userService.CheckIfEmailExists(request.Email).Result)
            {
                return Task.FromResult(new CreateCustomerResult { Email = "", Username = "", Name = "" });
            }
            var result = _userService.GetUserByUsername(request.Username).Result;
            return Task.FromResult(new CreateCustomerResult { Email = result.Email, Name = result.Name, Username = result.Username });
        }

        public override Task<CheckUserResult> CheckIfUserIsRegistered(CheckUserRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CheckUserResult
            {
                IsRegistered = _userService.CheckIfUserIsRegistered(request.Username, request.Password).Result
            });
        }

        public override Task<CreateCustomerResult> GetUserByUsername(GetUserRequest request, ServerCallContext context)
        {
            var result = _userService.GetUserByUsername(request.Username).Result;
            return Task.FromResult(new CreateCustomerResult { Email = result.Email, Name = result.Name, Username = result.Username });
        }

        public override Task<ListCreateCustomerResult> GetUsersBeforeDate(GetUsersBeforeDateRequest request, ServerCallContext context)
        {
            return Mapper.Map(_userService.GetUsersBeforeDate(request.ExpirationDate.ToDateTime()).Result);
        }
    }
}
