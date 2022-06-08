using MediatR;
using Microservices.Infra.DBContext.Entities;

namespace Microservices.Api.Notifications.Demo
{
    public record DemoAddedNotification(Enterprise_MVC_Core Enterprise_MVC_Core) : INotification;
}
