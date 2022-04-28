using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Core.ViewModel.Account.Base
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool RememberLogin { get; set; } = false;
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
