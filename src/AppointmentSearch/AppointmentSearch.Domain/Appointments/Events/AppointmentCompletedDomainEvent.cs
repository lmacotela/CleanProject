using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Appointments.Events
{
    public sealed record AppointmentCompletedDomainEvent(Guid AppointmentId) : IDomainEvent;
}