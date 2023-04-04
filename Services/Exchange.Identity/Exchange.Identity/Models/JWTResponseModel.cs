using System.Net;

namespace Exchange.Identity.Models
{
    public class JWTResponseModel
    {
        public bool IsExpired { get; set; }
        public bool IsVerified { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
