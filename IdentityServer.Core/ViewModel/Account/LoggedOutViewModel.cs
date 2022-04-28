namespace IdentityServer.Core.ViewModel.Account.UI
{
    public class LoggedOutViewModel
    {
        public string PostLogoutRedirectUri { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string SignOutIframeUrl { get; set; } = string.Empty;

        public bool AutomaticRedirectAfterSignOut { get; set; } = false;

        public string LogoutId { get; set; } = string.Empty;
        public bool TriggerExternalSignout => ExternalAuthenticationScheme is not null;
        public string ExternalAuthenticationScheme { get; set; } = string.Empty;
    }
}
