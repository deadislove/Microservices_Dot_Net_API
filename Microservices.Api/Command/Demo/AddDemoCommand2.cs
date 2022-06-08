using MediatR;
using Microservices.Infra.DBContext.Entities;

namespace Microservices.Api.Command.Demo
{
    public record AddDemoCommand2(Enterprise_MVC_Core Enterprise_MVC_Core) : IRequest<Enterprise_MVC_Core>;
}
