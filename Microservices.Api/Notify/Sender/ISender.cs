using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Api.Notify.Sender
{
    public interface ISender
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
        Task<object?> Send(object request, CancellationToken cancellationToken = default);
    }
}
