using MediatR;
using Microservices.Api.Notifications.Demo;
using Microservices.Core.Services.Demo;
using Microservices.Infra.DBContext.Entities;
using Microservices.Infra.DBContext.Interface;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Api.Handlers.Demo
{
    public class CacheInvalidationHandler : INotificationHandler<DemoAddedNotification>
    {
        private readonly DemoServices _demo;
        private readonly ILogger<Enterprise_MVC_Core> _logger;

        public CacheInvalidationHandler(IGenericTypeRepository<Enterprise_MVC_Core> repo, ILogger<Enterprise_MVC_Core> logger)
        {
            _demo = new DemoServices(repo);
            _logger = logger;
        }

        public async Task Handle(DemoAddedNotification notification, CancellationToken cancellationToken)
        {
            await _demo.EventOccured(notification.Enterprise_MVC_Core, "Cache Invalidated");
            await Task.CompletedTask;            
        }
    }
}
