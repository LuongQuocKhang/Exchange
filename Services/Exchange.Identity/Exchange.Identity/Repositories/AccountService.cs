using Azure.Core;
using Duende.IdentityServer.Models;
using Exchange.Identity.Data;
using Exchange.Identity.Infrastructure;
using Exchange.Identity.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace Exchange.Identity.Repositories
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AccountService(ILogger<AccountService> logger, AppDbContext appDbContext,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration;
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

            var accessTokenInfo = _appDbContext.TBL_ADM_JWT_WHITE_LIST
                                                .FirstOrDefault(x => x.AccessToken == model.AccessToken);

            if (accessTokenInfo == null)
            {
                response.Message = $"Access Token for {model.UserName} not exist in database";
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                _logger.LogError($"Access Token for {model.UserName} not exist in database");

                return response;
            }
            else
            {
                if (accessTokenInfo.IsExpired)
                {
                    await RevokeAccessTokenAsync(model.AccessToken);

                    response.IsExpired = true;

                    _logger.LogError($"{model.UserName} access Expired Token in database => Revoke All Token");

                    return response;
                }

                // Check if token expired
                DateTime accessTokenExpiredIn = accessTokenInfo.AccessTokenExpiredIn;
                if (DateTime.Now > accessTokenExpiredIn)
                {
                    DateTime refreshTokenExpiredIn = accessTokenInfo.RefreshTokenExpiredIn;

                    if(DateTime.Now > refreshTokenExpiredIn)
                    {
                        // return to login page
                    }
                    else
                    {
                        // use refresh token to get new token
                        var client = new HttpClient();

                        var tokenResponse = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest()
                        {
                            Address = "https://localhost:8000/connect/authorize",
                            ClientId = "45786368616e6765436c69656e74",
                            ClientSecret = "45786368616e6765436c69656e7453656372656374",
                            GrantType = GrantType.AuthorizationCode
                        });

                        if (!tokenResponse.IsError)
                        {
                            //tokenResponse.
                        }
                    }
                }
                else
                {
                    var client = new HttpClient();

                    var tokenResponse = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest()
                    {
                        Address = "https://localhost:8000/connect/authorize",
                        ClientId = "45786368616e6765436c69656e74",
                        ClientSecret = "45786368616e6765436c69656e7453656372656374",
                        GrantType = GrantType.AuthorizationCode
                    });

                    if (!tokenResponse.IsError)
                    {
                        //tokenResponse.
                    }
                }
            }

            return response;
        }

        public async Task<JWTResponseModel> SignInWithEmailAndPasswordAsync(SignInModel model)
        {
            var response = new JWTResponseModel();
            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                _logger.LogError($"User {model.UserName} not found.");
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = $"User {model.UserName} not found.";
                return response;
            }

            var signIn = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (signIn.Succeeded)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = $"{model.UserName} sign in successfully";
                response.IsVerified = true;

                // Create new token
                var newToken = await AddNewJWTTokenToWhiteListAsync(model.UserName);
                response.AccessToken = newToken;
            }
            return response;
        }

        public async Task<JWTResponseModel> SignUpWithEmailAndPasswordAsync(SignUpModel model)
        {
            var response = new JWTResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.UserName
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);
                if(result.Succeeded)
                {
                    response.Message = $"User {model.UserName} was created successfully.";
                    return response;
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.Message = result.Errors.First().Description;
                    return response;
                }
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = $"User {model.UserName} already exist in database.";
                return response;
            }
        }

        private async Task RevokeAccessTokenAsync(string access_token)
        {
            await Task.Delay(1000);
        }

        private async Task<string> AddNewJWTTokenToWhiteListAsync(string userName)
        {
            var client = new HttpClient();

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:8000/connect/token",

                ClientId = "45786368616e6765436c69656e74",
                ClientSecret = "45786368616e6765436c69656e7453656372656374",
                Scope = "Exchange",
                GrantType = GrantType.ClientCredentials
            });

            if (!tokenResponse.IsError)
            {
                string newAccessToken = tokenResponse.AccessToken;
                DateTime accessTokenExpiredIn = DateTime.Now.AddMinutes(tokenResponse.ExpiresIn);

                var newTokenWhiteList = new TBL_ADM_JWT_WHITE_LIST()
                {
                    AccessToken = newAccessToken,
                    AccessTokenExpiredIn = accessTokenExpiredIn,
                    IsExpired = false,
                    UserId = userName,
                    RefreshToken = tokenResponse.RefreshToken,
                    RefreshTokenExpiredIn = DateTime.Now.AddMinutes(60)
                };

                _appDbContext.TBL_ADM_JWT_WHITE_LIST.Add(newTokenWhiteList);
                _appDbContext.SaveChanges();

                return newAccessToken;
            }

            return string.Empty;
        }
    }
}
