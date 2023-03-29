namespace Exchange.Identity.API.Models
{
    public class JWTResponse
    {
        public string user_name { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public DateTime access_token_expired_in { get; set; }
        public DateTime refresh_token_expired_in { get; set; }
        public bool is_verified { get; set; }
        public bool is_expired { get; set; }
    }

    public class JWTRequestAuthorize
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}
