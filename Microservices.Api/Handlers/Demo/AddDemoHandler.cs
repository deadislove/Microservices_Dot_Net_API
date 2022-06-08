using MediatR;
using Microservices.Api.Command.Demo;
using Microservices.Core.Services.Demo;
using Microservices.Infra.DBContext.Entities;
using Microservices.Infra.DBContext.Interface;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Api.Handlers.Demo
{
    public partial class AddDemoHandler : IRequestHandler<AddDemoCommand, Unit>
    {
        private readonly DemoServices _demo;
        private readonly ILogger<Enterprise_MVC_Core> _logger;

        public AddDemoHandler(IGenericTypeRepository<Enterprise_MVC_Core> repo, ILogger<Enterprise_MVC_Core> logger)
        {
            _demo = new DemoServices(repo);
            _logger = logger;
        }

        public async Task<Unit> Handle(AddDemoCommand request, CancellationToken cancellationToken)
        {
            // Add the service's add function.
            return Unit.Value;
        }
    }

    public partial class AddDemoHandler : IRequestHandler<AddDemoCommand2, Enterprise_MVC_Core>
    {
        public async Task<Enterprise_MVC_Core> Handle(AddDemoCommand2 request, CancellationToken cancellationToken)
        {
            // Add the service's add function.

            return request.Enterprise_MVC_Core;
        }
    }
}
