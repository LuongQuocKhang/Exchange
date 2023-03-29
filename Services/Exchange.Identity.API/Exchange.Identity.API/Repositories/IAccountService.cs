using Exchange.Identity.API.Models;
using Firebase.Auth;

namespace Exchange.Identity.API.Repositories
{
    public interface IAccountService
    {
        Task<JWTResponse> SignInWithEmailAndPasswordAsync(string email, string password);
        Task<bool> CreateUserWithEmailAndPasswordAsync(SignUpModel signUpModel);
    }
}
