using MediatR;
using Microservices.Api.Command.Demo;
using Microservices.Api.Notifications.Demo;
using Microservices.Api.Queries.Demo;
using Microservices.Infra.DBContext.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Microservices.Api.Controllers.MediatR
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediatRDemoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MediatRDemoController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Enterprise_MVC_Core>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetMediatRDemo()
        {
            var _obj = await _mediator.Send(new GetDemoQuery());

            return Ok(_obj);
        }

        /// <summary>
        /// Get object by ID parameters.
        /// </summary>
        /// <param name="id">ID index</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name ="GetRediatRDemoById")]
        [ProducesResponseType(typeof(Enterprise_MVC_Core), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetMediatRDemoById(int id)
        {
            var _obj = await _mediator.Send(new GetDemoByIdQuery(id));

            return Ok(_obj);
        }

        /// <summary>
        /// Post method - return http status code.
        /// </summary>
        /// <param name="enterprise_MVC_Core">DB entity schema</param>
        /// <response code="201">Object created</response>
        /// <response code="400">Bad requests.</response>
        /// <returns>Http status code 201</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddMediatRDemo([FromBody] Enterprise_MVC_Core enterprise_MVC_Core)
        {
            await _mediator.Send(new AddDemoCommand(enterprise_MVC_Core));

            return StatusCode(201);
        }

        /// <summary>
        /// Post method - return new object.
        /// </summary>
        /// <param name="enterprise_MVC_Core"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMediatRDemo2")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddMediatRDemo2([FromBody] Enterprise_MVC_Core enterprise_MVC_Core)
        {
            var _objReturn = await _mediator.Send(new AddDemoCommand2(enterprise_MVC_Core));

            await _mediator.Publish(new DemoAddedNotification(enterprise_MVC_Core));

            return CreatedAtRoute("GetMediatRDemoById", new { Id = _objReturn.ID }, _objReturn);
        }
    }
}
