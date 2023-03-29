namespace Exchange.Identity.GRPC.Entities
{
    public class JWTResponseData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiredIn { get; set; }
        public DateTime RefreshTokenExpiredIn { get; set; }
        public string Message { get; set; }
        public bool IsExpired { get; set; }
        public bool IsVerified { get; set; }
    }
}
