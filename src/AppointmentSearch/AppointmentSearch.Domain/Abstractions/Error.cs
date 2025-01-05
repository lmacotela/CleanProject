namespace AppointmentSearch.Domain.Abstractions
{
    public record Error(string Code, string Name)
    {
        public static Error None => new Error(string.Empty, string.Empty);
        public static Error NullValue => new Error("Error.NullValue", "a null value was entered");
        public static Error NotFound => new Error("NotFound", "Resource not found");
    }
}