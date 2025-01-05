using AppointmentSearch.Domain.Abstractions;

namespace AppointmentSearch.Domain.Appointments
{
    public static class AppointmentErrors
    {
        public static Error NotFound =
            new Error("Appointment.Found", $"Appointment was not found");
        public static Error Overlap =
            new Error("Appointment.Overlap", $"The appointment is being taken by 2 or more patients at the same time on the same date");
        public static Error NotReserved= 
            new Error("Appointment.NotReserved", $"The appointment is not reserved");
        public static Error NotConfirmed=
            new Error("Appointment.NotConfirmed", $"The appointment is not confirmed");
        public static Error AlreadyStarted=
            new Error("Appointment.AllreadyStarted", $"The appointment has already started");
    }
}