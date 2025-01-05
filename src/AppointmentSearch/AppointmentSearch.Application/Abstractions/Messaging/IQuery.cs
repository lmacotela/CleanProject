using AppointmentSearch.Domain.Abstractions;
using MediatR;

namespace AppointmentSearch.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {

    }
}