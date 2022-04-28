using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.SharedKernel.HttpContext
{
    /// <summary>
    /// In .NET Core or .NET5 and other new version, HttpContext can't directly to use, 
    /// so I create the extension method which can directly use the HttpContext as ASP.NET HttpContext.
    /// </summary>
    public static class HttpContextExtension
    {
        public static IServiceCollection ServiceCollection { get; set; }

        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                object factory = ServiceCollection.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));
                Microsoft.AspNetCore.Http.HttpContext context = ((HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
    }
}
