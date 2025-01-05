using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Appointments.Events
{
    public sealed record AppointmentRejectedDomainEvent(Guid AppointmentId) : IDomainEvent;
}