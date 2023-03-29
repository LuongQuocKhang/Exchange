using Exchange.Identity.GRPC.Protos;
using static Exchange.Identity.GRPC.Protos.AccountProtoService;

namespace Exchange.Identity.API.GrpcService
{
    public class ExchangeIdentityGrpcService
    {
        private readonly AccountProtoService.AccountProtoServiceClient _accountProtoServiceClient;

        public ExchangeIdentityGrpcService(AccountProtoService.AccountProtoServiceClient accountProtoServiceClient)
        {
            _accountProtoServiceClient = accountProtoServiceClient ?? throw new ArgumentNullException(nameof(accountProtoServiceClient));
        }

        public async Task<AddJWTTokenToWhiteListResponse> AddJWTTokenToWhiteList(AddJWTToken request)
        {
            var result = await _accountProtoServiceClient.AddJWTTokenToWhiteListAsync(request);
            return result;
        }

        public async Task<JWTResponse> AuthenticateJWTToken(AuthenticateJWT request)
        {
            var result = await _accountProtoServiceClient.AuthenticateJWTTokenAsync(request);
            return result;
        }

    }
}
