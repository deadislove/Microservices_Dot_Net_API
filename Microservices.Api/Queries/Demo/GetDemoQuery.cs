using MediatR;
using Microservices.Infra.DBContext.Entities;
using System.Collections.Generic;

namespace Microservices.Api.Queries.Demo
{
    public record GetDemoQuery() : IRequest<IEnumerable<Enterprise_MVC_Core>>;
}
