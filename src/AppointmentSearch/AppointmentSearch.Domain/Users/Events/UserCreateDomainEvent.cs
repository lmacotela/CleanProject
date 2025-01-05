using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Users.Events
{
    public sealed record UserCreateDomainEvent (Guid id) : IDomainEvent;
}