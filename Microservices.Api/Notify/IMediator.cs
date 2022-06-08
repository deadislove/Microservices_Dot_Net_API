using Microservices.Api.Notify.Publisher;
using Microservices.Api.Notify.Sender;

namespace Microservices.Api.Notify
{
    public interface IMediator: ISender, IPublisher
    {
    }
}
