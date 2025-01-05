using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Doctors
{
    public static class DoctorErrors
    {
        public static Error NotFound => new Error("Doctor.Found", "Doctor not found.");

    }
}