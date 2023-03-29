using Exchange.Identity.API.Common;
using Exchange.Identity.API.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace Exchange.Identity.API.Repositories
{
    public class AccountService : IAccountService
    {
        private readonly FirebaseAuthClient _firebaseAuthClient;

        public AccountService(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }

        public async Task<JWTResponse> SignInWithEmailAndPasswordAsync(string email, string password)
        {
            var userCredential = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);

            var result = new JWTResponse()
            {
                user_name = email,
                access_token = userCredential.User != null ? userCredential.User.Credential.IdToken : "",
                refresh_token = userCredential.User != null ? userCredential.User.Credential.RefreshToken : "",
                access_token_expired_in = DateTime.UtcNow.AddMinutes(TokenLifeTIme._5_MINUTES).AddHours(7),
                refresh_token_expired_in = DateTime.UtcNow.AddMinutes(TokenLifeTIme._15_MINUTES).AddHours(7),
                is_verified = true,
                is_expired = false
            };

            return result;
        }

        public async Task<bool> CreateUserWithEmailAndPasswordAsync(SignUpModel signUpModel)
        {
            var userCredential = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(signUpModel.UserName, signUpModel.Password, displayName: signUpModel.FullName);

            return userCredential.User != null;
        }
    }
}
