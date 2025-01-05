using AppointmentSearch.Domain.Abstractions;
using AppointmentSearch.Domain.Appointments.Events;
using AppointmentSearch.Domain.Doctors;
using AppointmentSearch.Domain.Shared;

namespace AppointmentSearch.Domain.Appointments
{
    public sealed class Appointment : Entity
    {
        private Appointment() { }
        private Appointment(
            Guid id,
            Guid doctorId,
            Guid userId,
            DateRange period,
            Money price,
            Status status,
             DateTime createdOnUtc
            ) : base(id)
        {
            DoctorId = doctorId;
            UserId = userId;
            Period = period;
            Price = price;
            Status = status;
            CreatedOnUtc = createdOnUtc;
        }
        public Guid DoctorId { get; private set; }
        public Guid UserId { get; private set; }
        public DateRange Period { get; private set; }
        public Money? Price { get; private set; }
        public Status Status { get; private set; } = new();
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ConfirmationDate { get; private set; }
        public DateTime? RejectedDate { get; private set; }
        public DateTime? CompletedDate { get; private set; }
        public DateTime? CancelationDate { get; private set; }

        public static Appointment Reserve(
            Doctor doctor,
            Guid userId,
            DateRange period,
            DateTime utcNow,
            PricingService PricingService
            )
        {
            var pricingDetails = PricingService.CalculatePrice(doctor, period);
            var appointment = new Appointment(
                Guid.NewGuid(),
                doctor.Id,
                userId,
                period,
                pricingDetails.TotalPrice,
                Status.Reserved,
                utcNow
                );
            appointment.RaiseDomainEvent(new AppointmentReservedDomainEvent(appointment.Id!));
            doctor.LastAppointmentDate = utcNow;
            return appointment;
        }

        public Result Confirm(DateTime utcNow)
        {
            if (Status != Status.Reserved)
            {
                return Result.Failure(AppointmentErrors.NotReserved);
            }
            Status = Status.Confirmed;
            ConfirmationDate = utcNow;
            RaiseDomainEvent(new AppointmentConfirmedDomainEvent(Id));
            return Result.Success();
        }
        public Result Reject(DateTime utcNow)
        {
            if (Status != Status.Reserved)
            {
                return Result.Failure(AppointmentErrors.NotReserved);
            }
            Status = Status.Denied;
            RejectedDate = utcNow;
            RaiseDomainEvent(new AppointmentRejectedDomainEvent(Id));
            return Result.Success();
        }
        public Result Complete(DateTime utcNow)
        {
            if (Status != Status.Confirmed)
            {
                return Result.Failure(AppointmentErrors.NotConfirmed);
            }
            Status = Status.Completed;
            CompletedDate = utcNow;
            RaiseDomainEvent(new AppointmentCompletedDomainEvent(Id));
            return Result.Success();
        }
        public Result Cancel(DateTime utcNow)
        {
            if (Status != Status.Confirmed)
            {
                return Result.Failure(AppointmentErrors.NotConfirmed);
            }
            var currentDate = DateOnly.FromDateTime(utcNow);
            if(currentDate > Period.Start)
            {
                return Result.Failure(AppointmentErrors.AlreadyStarted);
            }

            Status = Status.Canceled;
            CancelationDate = utcNow;
            RaiseDomainEvent(new AppointmentCanceledDomainEvent(Id));
            return Result.Success();
        }
    }
}