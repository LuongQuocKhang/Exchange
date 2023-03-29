using AutoMapper;
using Exchange.Identity.GRPC.Repositories;
using Exchange.Identity.GRPC.Protos;
using Grpc.Core;

namespace Exchange.Identity.GRPC.Services
{
    public class AccountService : AccountProtoService.AccountProtoServiceBase
    {
        private readonly IAccountRepository _repository;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repository, ILogger<AccountService> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<AddJWTTokenToWhiteListResponse> AddJWTTokenToWhiteList(AddJWTToken request, ServerCallContext context)
        {
            await _repository.AddJWTTokenToWhiteListAsync(request);
            _logger.LogInformation($"Token {request.AccessToken} is added to redis cache.");

            return new AddJWTTokenToWhiteListResponse()
            {
                Success = true
            };
        }

        public override async Task<JWTResponse> AuthenticateJWTToken(AuthenticateJWT request, ServerCallContext context)
        {
            var authorizedToken = await _repository.AuthorizeJWTTokenAsync(request);

            var result = _mapper.Map<JWTResponse>(authorizedToken);

            if (authorizedToken.IsVerified)
            {
                _logger.LogInformation($"Token {authorizedToken.AccessToken} is verified.");
            }
            else
            {
                _logger.LogError($"Token {authorizedToken.AccessToken} is not verified. {authorizedToken.Message}");
            }

            return result;
        }
    }
}
