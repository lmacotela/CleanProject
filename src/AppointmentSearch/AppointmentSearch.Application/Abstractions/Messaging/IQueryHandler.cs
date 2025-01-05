using AppointmentSearch.Domain.Abstractions;
using MediatR;

namespace AppointmentSearch.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery ,TResponse> : IRequestHandler<TQuery ,Result<TResponse>>
    where TQuery : IQuery<TResponse>
    {
        
    }
}