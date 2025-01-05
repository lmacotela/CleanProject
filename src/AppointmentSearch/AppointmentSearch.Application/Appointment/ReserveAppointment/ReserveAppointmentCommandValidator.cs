using FluentValidation;

namespace AppointmentSearch.Application.Appointment.ReserveAppointment;
public class ReserveAppointmentCommandValidator : AbstractValidator<ReserveAppointmentCommand>
{
    public ReserveAppointmentCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate);
    }
}