using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Appointments.Events
{
    public sealed record AppointmentConfirmedDomainEvent(Guid AppointmentId) : IDomainEvent;
}