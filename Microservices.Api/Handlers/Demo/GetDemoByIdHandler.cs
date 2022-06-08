using MediatR;
using Microservices.Api.Queries.Demo;
using Microservices.Core.Services.Demo;
using Microservices.Infra.DBContext.Entities;
using Microservices.Infra.DBContext.Interface;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Api.Handlers.Demo
{
    public class GetDemoByIdHandler : IRequestHandler<GetDemoByIdQuery, Enterprise_MVC_Core>
    {
        private readonly DemoServices _demo;
        private readonly ILogger<Enterprise_MVC_Core> _logger;

        public GetDemoByIdHandler(IGenericTypeRepository<Enterprise_MVC_Core> repo, ILogger<Enterprise_MVC_Core> logger)
        {
            _demo = new DemoServices(repo);
            _logger = logger;
        }

        public async Task<Enterprise_MVC_Core> Handle(GetDemoByIdQuery request, CancellationToken cancellationToken)
        {
            var _obj = _demo.GetAll();
            var _returnObj = _obj.Where(w => w.ID.Equals(request.Id)).FirstOrDefault();
            return _returnObj is null ? new Enterprise_MVC_Core() : _returnObj;            
        }
    }
}
