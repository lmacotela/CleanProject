namespace AppointmentSearch.Application.Appointment.GetAppointmet;
public sealed class AppointmentResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid DoctorId { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public string? Status { get; init; }
    public decimal Price { get; init; }
    public string? Currency { get; init; }
    public DateTime CreatedOnUtc { get; init; }
}
