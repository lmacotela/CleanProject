using AppointmentSearch.Domain.Abstractions;
using AppointmentSearch.Domain.Appointments;

namespace AppointmentSearch.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review() { }
        private Review(
            Guid id,
            Guid doctorId,
            Guid userId,
            Guid appointmentId,
            Comment? comentary,
            Rating rating,
            DateTime createdDate
            ) : base(id)
        {
            DoctorId = doctorId;
            UserId = userId;
            AppointmentId = appointmentId;
            Comentary = comentary;
            Rating = rating;
            CreatedDate = createdDate;
        }
        public Guid DoctorId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid AppointmentId { get; private set; }
        public Comment? Comentary { get; private set; }
        public Rating Rating { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public static Result<Review> Create(
            Appointment appointment,
            Comment? comentary,
            Rating rating,
            DateTime createdDate
            )
        {
            if (appointment.Status != Status.Completed)
                return Result.Failure<Review>(ReviewErrors.NotElegible);

            var review = new Review(
                Guid.NewGuid(),
                appointment.DoctorId,
                appointment.UserId,
                appointment.Id,
                comentary,
                rating,
                createdDate
                );

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));
            return review;
        }
    }
}