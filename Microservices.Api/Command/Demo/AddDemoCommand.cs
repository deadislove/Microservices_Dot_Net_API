using MediatR;
using Microservices.Infra.DBContext.Entities;

namespace Microservices.Api.Command.Demo
{
    public record AddDemoCommand(Enterprise_MVC_Core Enterprise_MVC_Core) : IRequest;
}
