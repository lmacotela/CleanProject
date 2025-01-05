namespace AppointmentSearch.Api.Controllers.Appointments;
public sealed record AppointmentReserveRequest(
    Guid DoctorId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
);
