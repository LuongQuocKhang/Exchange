namespace Exchange.Identity.Data
{
    public class TBL_ADM_JWT_WHITE_LIST : BaseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiredIn { get; set; }
        public DateTime RefreshTokenExpiredIn { get; set; }
        public bool IsExpired { get; set; }
        public string UserId { get; set; }
    }
}
