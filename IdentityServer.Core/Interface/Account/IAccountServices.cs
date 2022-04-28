using IdentityServer.Core.ViewModel.Account.Base;
using IdentityServer.Core.ViewModel.Account.UI;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Core.Interface.Account
{
    public interface IAccountServices : IDisposable
    {
        public Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl);

        public Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model);

        public Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId, ClaimsPrincipal User, HttpContext httpContext);
    }
}
