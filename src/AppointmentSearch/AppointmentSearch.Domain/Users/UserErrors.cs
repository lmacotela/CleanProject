using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound => new Error("User.Found", "User not found.");
        public static Error InvalidCredentials => new Error("User.InvalidCredentials", "Invalid credentials.");
    }
}