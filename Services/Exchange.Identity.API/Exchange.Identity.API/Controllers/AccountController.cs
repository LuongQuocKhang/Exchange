using AutoMapper;
using Exchange.Identity.API.GrpcService;
using Exchange.Identity.API.Models;
using Exchange.Identity.API.Repositories;
using Exchange.Identity.GRPC.Protos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Exchange.Identity.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ExchangeIdentityGrpcService _exchangeIdentityGrpcService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper, ExchangeIdentityGrpcService exchangeIdentityGrpcService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _exchangeIdentityGrpcService = exchangeIdentityGrpcService ?? throw new ArgumentNullException(nameof(exchangeIdentityGrpcService));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<RequestReponseModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RequestReponseModel>> SignInWithEmailAndPasswordAsync([FromBody] SignInModel signInModel)
        {
            
            var response = new RequestReponseModel();
            try
            {
                var jwtResponse = await _accountService.SignInWithEmailAndPasswordAsync(signInModel.UserName, signInModel.Password);
                if(jwtResponse.is_verified)
                {
                    var addJWTToken = _mapper.Map<AddJWTToken>(jwtResponse);
                    addJWTToken.Password = signInModel.Password;
                    await _exchangeIdentityGrpcService.AddJWTTokenToWhiteList(addJWTToken);
                }
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Content = jwtResponse;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(response);
        }


        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<RequestReponseModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RequestReponseModel>> SignUpWithEmailAndPasswordAsync([FromBody] SignUpModel signUpModel)
        {

            var response = new RequestReponseModel();
            try
            {
                if(signUpModel.Password != signUpModel.ConfirmPassword)
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.IsSuccess = false;
                    response.ErrorMessage = "Confirm Password Not Match";
                }
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = await _accountService.CreateUserWithEmailAndPasswordAsync(signUpModel);
                response.Content = "";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(response);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<RequestReponseModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RequestReponseModel>> AuthorizeJWTToken([FromBody] JWTRequestAuthorize requestData)
        {

            var response = new RequestReponseModel();
            try
            {
                var authenticateJWT = _mapper.Map<AuthenticateJWT>(requestData);
                var jwtResponse = await _exchangeIdentityGrpcService.AuthenticateJWTToken(authenticateJWT);
                if (jwtResponse.IsVerified)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = "Token verified successfully";
                    response.IsSuccess = true;
                } 
                else
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = $"Token verified failed, Token Expierd: {jwtResponse.IsExpired}";
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(response);
        }
    }
}
