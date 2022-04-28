using Microservices.Api.Logging.Interface;
using Microservices.Core.Services.Demo;
using Microservices.Infra.DBContext.Entities;
using Microservices.Infra.DBContext.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Microservices.Api.Controllers.Demo
{
    [ServiceFilter(typeof(ILogRepository))]
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly DemoServices demo;
        private readonly ILogger<Enterprise_MVC_Core> _logger;

        public DemoController(IGenericTypeRepository<Enterprise_MVC_Core> repo, ILogger<Enterprise_MVC_Core> logger)
        {
            demo = new DemoServices(repo);
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Enterprise_MVC_Core> Get()
        {
            _logger.LogInformation("Executing Demo controller - Get method without authorize.");
            return demo.Demo();
        }

        [HttpGet]
        [Route("GetWithAuthorize")]
        [Authorize(Roles = "admin")]
        public IEnumerable<Enterprise_MVC_Core> GetWithAuthorize()
        {
            _logger.LogInformation("Executing Demo controller - Get method with authorize.");
            return demo.Demo();
        }
    }
}
