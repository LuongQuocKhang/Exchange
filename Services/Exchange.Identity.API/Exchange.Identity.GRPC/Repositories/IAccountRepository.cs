using Exchange.Identity.GRPC.Entities;
using Exchange.Identity.GRPC.Model;
using Exchange.Identity.GRPC.Protos;

namespace Exchange.Identity.GRPC.Repositories
{
    public interface IAccountRepository
    {
        Task<JWTResponseData> AuthorizeJWTTokenAsync(AuthenticateJWT request);
        Task<AddJWTTokenToWhiteListResponse> AddJWTTokenToWhiteListAsync(AddJWTToken request);
        Task RevokeAccessTokenAsync(string userName);
    }
}
