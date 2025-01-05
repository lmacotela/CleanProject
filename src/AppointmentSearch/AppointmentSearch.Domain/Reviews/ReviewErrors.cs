using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Reviews
{
    public static class ReviewErrors
    {
        public static readonly Error NotElegible = new Error("Review.NotElegible", "You can not make a review because you didn't reserve the appointment.");
    }
}