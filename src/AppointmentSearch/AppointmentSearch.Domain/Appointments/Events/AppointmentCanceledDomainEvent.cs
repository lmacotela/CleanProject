using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Appointments.Events
{
    public sealed record AppointmentCanceledDomainEvent(Guid AppointmentId) : IDomainEvent;
}