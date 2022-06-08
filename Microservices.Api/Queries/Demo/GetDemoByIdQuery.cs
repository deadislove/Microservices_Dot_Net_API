using MediatR;
using Microservices.Infra.DBContext.Entities;

namespace Microservices.Api.Queries.Demo
{
    public record GetDemoByIdQuery(int Id) : IRequest<Enterprise_MVC_Core>;
}
