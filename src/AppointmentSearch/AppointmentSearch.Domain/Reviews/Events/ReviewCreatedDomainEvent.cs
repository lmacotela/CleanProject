using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Reviews
{
    public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
}