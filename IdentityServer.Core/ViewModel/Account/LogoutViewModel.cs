using IdentityServer.Core.ViewModel.Account.Base;

namespace IdentityServer.Core.ViewModel.Account.UI
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }
}
