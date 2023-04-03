using Exchange.Identity.Models;

namespace Exchange.Identity.Repositories
{
    public interface IAccountService
    {
        Task<JWTResponseModel> SignInWithEmailAndPasswordAsync(SignInModel model);
        Task<JWTResponseModel> SignUpWithEmailAndPasswordAsync(SignUpModel model);
        Task<JWTResponseModel> AuthorizeJWTToken(JWTRequestAuthorize model);
    }
}
