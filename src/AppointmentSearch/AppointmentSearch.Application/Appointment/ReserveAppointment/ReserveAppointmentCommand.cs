using AppointmentSearch.Application.Abstractions.Messaging;

namespace  AppointmentSearch.Application.Appointment.ReserveAppointment;

public record ReserveAppointmentCommand (
    Guid DoctorId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
) : ICommand<Guid>;