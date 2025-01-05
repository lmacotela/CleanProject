using AppointmentSearch.Application.Abstractions.Clock;
using AppointmentSearch.Application.Abstractions.Messaging;
using AppointmentSearch.Application.Exceptions;
using AppointmentSearch.Domain.Abstractions;
using AppointmentSearch.Domain.Appointments;
using AppointmentSearch.Domain.Doctors;
using AppointmentSearch.Domain.Users;

namespace AppointmentSearch.Application.Appointment.ReserveAppointment;

internal sealed class ReserveAppointmentCommandHandler : ICommandHandler<ReserveAppointmentCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly PricingService _pricingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReserveAppointmentCommandHandler(IUserRepository userRepository, IDoctorRepository doctorRepository
    , IAppointmentRepository appointmentRepository
    , PricingService pricingService
    , IUnitOfWork unitOfWork
    , IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _doctorRepository = doctorRepository;
        _appointmentRepository = appointmentRepository;
        _pricingService = pricingService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(ReserveAppointmentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<Guid>.Failure<Guid>(UserErrors.NotFound);
        }

        var doctor = await _doctorRepository.GetAsync(request.DoctorId, cancellationToken);

        if (doctor is null)
        {
            return Result<Guid>.Failure<Guid>(DoctorErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _appointmentRepository.IsOverlappingAsync(doctor, duration, cancellationToken))
        {
            return Result<Guid>.Failure<Guid>(AppointmentErrors.Overlap);
        }

        try
        {
            var appointment = AppointmentSearch.Domain.Appointments.Appointment.Reserve(doctor, user.Id, duration, _dateTimeProvider.UtcNow, _pricingService);

            _appointmentRepository.Add(appointment);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return appointment.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(AppointmentErrors.Overlap);

        }

    }
}
