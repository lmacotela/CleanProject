using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Reviews
{
    public sealed record Rating
    {
        public static Error Invalid = new Error("Rating.Invalid", "The rating is invalid");
        private Rating(int value) => Value = value;
        public int Value { get; init; }
        public static Result<Rating> Create(int value)
        {
            if (value < 1 || value > 5)
                return Result.Failure<Rating>(Invalid);
            return new Rating(value);
        }
    }
}