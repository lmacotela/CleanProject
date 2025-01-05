using AppointmentSearch.Application.Abstractions.Messaging;

namespace AppointmentSearch.Application.Appointment.GetAppointmet;

public sealed record GetAppointmentQuery(Guid AppointmentId) : IQuery<AppointmentResponse>;