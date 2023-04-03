using Azure.Core;
using Exchange.Identity.Infrastructure;
using Exchange.Identity.Models;

namespace Exchange.Identity.Repositories
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IdentityDbContext _identityDbContext;

        public AccountService(ILogger<AccountService> logger, IdentityDbContext identityDbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityDbContext = identityDbContext ?? throw new ArgumentNullException(nameof(identityDbContext));
        }

        public async Task<JWTResponseModel> AuthorizeJWTToken(JWTRequestAuthorize model)
        {
            // 1. Check token in whitelist database
            // 1.1 if token not exist => return login page
            // 1.2 if token exist
            // 1.2.1 if token exist but expired and refresh_token is valid => generate new token, refresh_token
            // 1.2.2 if token exist and expired but called => revoke all token of that user
            // 1.2.3 return login page

            var response = new JWTResponseModel();

            var accessTokenInfo = _identityDbContext.TBL_ADM_JWT_WHITE_LIST.FirstOrDefault(x => x.AccessToken == model.AccessToken);

            if (accessTokenInfo == null)
            {
                response.Message = $"Access Token for {model.UserName} not exist in database";
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                _logger.LogError($"Access Token for {model.UserName} not exist in database");

                return response;
            }
            else
            {
                if (accessTokenInfo.IsExpired == true)
                {
                    await RevokeAccessTokenAsync(model.AccessToken);

                    response.IsExpired = true;

                    _logger.LogError($"{model.UserName} access Expired Token in database => Revoke All Token");

                    return response;
                }

                // Check if token expired
                
            }
        }

        public Task<JWTResponseModel> SignInWithEmailAndPasswordAsync(SignInModel model)
        {
            throw new NotImplementedException();
        }

        public Task<JWTResponseModel> SignUpWithEmailAndPasswordAsync(SignUpModel model)
        {
            throw new NotImplementedException();
        }

        private Task RevokeAccessTokenAsync(string access_token)
        {
            return 0;
        }
    }
}
