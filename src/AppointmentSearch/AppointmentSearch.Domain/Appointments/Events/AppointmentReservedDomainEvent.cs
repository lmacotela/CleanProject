using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Appointments.Events
{
    public sealed record AppointmentReservedDomainEvent(Guid AppointmentId) : IDomainEvent;
}