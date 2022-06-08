using MediatR;
using Microservices.Api.Queries.Demo;
using Microservices.Core.Services.Demo;
using Microservices.Infra.DBContext.Entities;
using Microservices.Infra.DBContext.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Api.Handlers.Demo
{
    public class GetDemoHandler : IRequestHandler<GetDemoQuery, IEnumerable<Enterprise_MVC_Core>>
    {
        private readonly DemoServices _demo;
        private readonly ILogger<Enterprise_MVC_Core> _logger;

        public GetDemoHandler(IGenericTypeRepository<Enterprise_MVC_Core> repo, ILogger<Enterprise_MVC_Core> logger)
        {
            _demo = new DemoServices(repo);
            _logger = logger;
        }

        public async Task<IEnumerable<Enterprise_MVC_Core>> Handle(GetDemoQuery request, CancellationToken cancellationToken)
        {
            return _demo.GetAll();
        }
    }
}
