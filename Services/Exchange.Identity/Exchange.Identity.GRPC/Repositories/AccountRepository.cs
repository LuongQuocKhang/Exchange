using FirebaseAdmin;
using Exchange.Identity.GRPC.Data;
using Microsoft.EntityFrameworkCore;
using Exchange.Identity.GRPC.Model;
using AutoMapper;
using Exchange.Identity.GRPC.Protos;
using Exchange.Identity.GRPC.Entities;
using Firebase.Auth;

namespace Exchange.Identity.GRPC.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger<AccountRepository> _logger;
        private readonly IMapper _mapper;
        private readonly FirebaseAuthClient _firebaseAuthClient;
        private readonly ExchangeIdentityDbContext _exchangeIdentityDbContext;
        private readonly FirebaseApp _firebaseApp;

        public AccountRepository(ILogger<AccountRepository> logger, IMapper mapper
            , FirebaseApp firebaseApp, ExchangeIdentityDbContext exchangeIdentityDbContext,
            FirebaseAuthClient firebaseAuthClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _firebaseApp = firebaseApp ?? throw new ArgumentNullException(nameof(firebaseApp));
            _exchangeIdentityDbContext = exchangeIdentityDbContext ?? throw new ArgumentNullException(nameof(exchangeIdentityDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _firebaseAuthClient = firebaseAuthClient ?? throw new ArgumentNullException(nameof(firebaseAuthClient));
        }

        public async Task<JWTResponseData> AuthorizeJWTTokenAsync(Protos.AuthenticateJWT request)
        {
            // 1. Check token in whitelist database
            // 1.1 if token not exist => return login page
            // 1.2 if token exist
            // 1.2.1 if token exist but expired and refresh_token is valid => generate new token, refresh_token
            // 1.2.2 if token exist and expired but called => revoke all token of that user
            // 1.2.3 return login page

            var result = _mapper.Map<JWTResponseData>(request);

            try
            {
                var accessTokenInfo = await _exchangeIdentityDbContext.JWTWhiteLists.FirstOrDefaultAsync(x => x.AccessToken == request.AccessToken);
                if (accessTokenInfo == null)
                {
                    result.Message = $"Access Token for {request.UserName} not exist in database";

                    _logger.LogError($"Access Token for {request.UserName} not exist in database");

                    return result;
                }
                else
                {
                    if (accessTokenInfo.IsExpired == true)
                    {
                        await RevokeAccessTokenAsync(request.UserName);

                        result.IsExpired = true;

                        _logger.LogError($"{request.UserName} access Expired Token in database => Revoke All Token");

                        return result;
                    }


                    // Check if token expired
                    DateTime accessTokenExpiredIn = accessTokenInfo.AccessTokenExpiredIn;
                    if (DateTime.Now > accessTokenExpiredIn)
                    {
                        await RevokeAccessTokenAsync(accessTokenInfo.UserName);
                        // Get last token
                        var userCredential = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(accessTokenInfo.UserName, accessTokenInfo.Password);
                        result.AccessToken = userCredential.User.Credential.IdToken;
                        result.RefreshToken = userCredential.User.Credential.RefreshToken;

                        await AddJWTTokenToWhiteListAsync(new AddJWTToken()
                        {
                            AccessToken = result.AccessToken,
                            RefreshToken = result.RefreshToken,
                            UserName = accessTokenInfo.UserName,
                            Password = accessTokenInfo.Password
                        });

                        // update old token to expired

                        request.IsExpired = false;
                        request.IsVerified = true;

                        _exchangeIdentityDbContext.SaveChanges();
                    }
                    else
                    {
                        result.IsVerified = true;
                        result.IsExpired = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsVerified = false;
            }
            return result;
        }

        public async Task<Protos.AddJWTTokenToWhiteListResponse> AddJWTTokenToWhiteListAsync(Protos.AddJWTToken request)
        {
            try
            {
                int index = 0;
                // remove all active token
                var accessTokenList = _exchangeIdentityDbContext.JWTWhiteLists.Where(x => x.UserName == request.UserName).ToList();

                foreach (var item in accessTokenList)
                {
                    var token = _exchangeIdentityDbContext.JWTWhiteLists.FirstOrDefault(x => x.AccessToken == item.AccessToken
                                                                                        && x.UserName == item.UserName);
                    if(token != null)
                    {
                        token.IsExpired = true;
                        index++;
                    }
                }

                // then add new token
                var whitelist = _mapper.Map<TBL_ADM_JWT_WHITE_LIST>(request);
                whitelist.WhiteListKey = request.UserName + request.AccessToken.Substring(0, 10) + index;
                whitelist.AccessTokenExpiredIn = DateTime.UtcNow.AddHours(7).AddMinutes(5);
                whitelist.RefreshTokenExpiredIn = DateTime.UtcNow.AddHours(7).AddMinutes(15);

                //_exchangeIdentityDbContext.JWTWhiteLists.Entry(whitelist).State = EntityState.Added;

                _exchangeIdentityDbContext.JWTWhiteLists.Add(whitelist);

                _exchangeIdentityDbContext.SaveChanges();

                return new AddJWTTokenToWhiteListResponse()
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RevokeAccessTokenAsync(string userName)
        {
            var accessTokenList = _exchangeIdentityDbContext.JWTWhiteLists.Where(x => x.UserName == userName
                                                                                && x.IsExpired == true).ToList();
            foreach (var item in accessTokenList)
            {
                item.IsExpired = true;
            }
            await _exchangeIdentityDbContext.SaveChangesAsync();
        }
    }
}
