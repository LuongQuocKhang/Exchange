using Exchange.Identity.Models;
using Exchange.Identity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Exchange.Identity.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<RequestReponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<RequestReponseModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<RequestReponseModel>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<RequestReponseModel>> SignInWithEmailAndPasswordAsync([FromBody] SignInModel signInModel)
        {

            var response = new RequestReponseModel();
            try
            {
                var result = await _accountService.SignInWithEmailAndPasswordAsync(signInModel);
                response.StatusCode = result.StatusCode;
                response.IsSuccess = result.StatusCode == HttpStatusCode.OK;
                response.ErrorMessage = result.Message;
                response.Content = result.AccessToken;

                return response;
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
        [ProducesResponseType(typeof(IEnumerable<RequestReponseModel>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RequestReponseModel>> SignUpWithEmailAndPasswordAsync([FromBody] SignUpModel signUpModel)
        {

            var response = new RequestReponseModel();
            try
            {
                if (signUpModel.Password != signUpModel.ConfirmPassword)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessage = "Confirm Password not match.";
                    return response;
                }

                var result = await _accountService.SignUpWithEmailAndPasswordAsync(signUpModel);
                response.StatusCode = result.StatusCode;
                response.IsSuccess = result.StatusCode == HttpStatusCode.OK;
                response.ErrorMessage = result.Message;
                return response;
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
                var jwtResponse = await _accountService.AuthorizeJWTToken(requestData);
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
