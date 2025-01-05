using System.Security.Cryptography.X509Certificates;
using AppointmentSearch.Application.Abstractions.Email;
using AppointmentSearch.Domain.Appointments;
using AppointmentSearch.Domain.Appointments.Events;
using AppointmentSearch.Domain.Users;
using MediatR;

namespace AppointmentSearch.Application.Appointment.ReserveAppointment;

internal sealed class ReserveAppointmentDomainEventHandler : INotificationHandler<AppointmentReservedDomainEvent>
{

    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ReserveAppointmentDomainEventHandler(IAppointmentRepository appointmentRepository, IUserRepository userRepository, IEmailService emailService)
    {
        _appointmentRepository = appointmentRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(AppointmentReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetAsync(notification.AppointmentId, cancellationToken);
        if (appointment is null)
        {
            return;
        }
        var user= await _userRepository.GetAsync(appointment.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }
        await _emailService.SendAsync(user.Email!, "Appointment Reserved", $"Your appointment has been reserved", cancellationToken);
    }
}