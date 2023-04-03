using Microsoft.AspNetCore.Mvc;

namespace Exchange.Identity.Models
{
    public class SignInModel
    {
        public string ReturnUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
