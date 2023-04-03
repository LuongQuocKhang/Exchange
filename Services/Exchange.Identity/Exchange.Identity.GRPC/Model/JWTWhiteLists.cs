using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exchange.Identity.GRPC.Model
{
    [Table("TBL_ADM_JWT_WHITE_LIST")]
    public class TBL_ADM_JWT_WHITE_LIST
    {
        [Key]
        public string WhiteListKey { get; set; }
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiredIn { get; set; }
        public DateTime RefreshTokenExpiredIn { get; set; }
        public bool IsExpired { get; set; }
    }
}
