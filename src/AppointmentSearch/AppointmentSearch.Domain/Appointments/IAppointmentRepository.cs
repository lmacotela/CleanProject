using AppointmentSearch.Domain.Doctors;

namespace AppointmentSearch.Domain.Appointments
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsOverlappingAsync(Doctor doctor, DateRange period, CancellationToken cancellationToken = default);
        void Add(Appointment appointment);
    }
}