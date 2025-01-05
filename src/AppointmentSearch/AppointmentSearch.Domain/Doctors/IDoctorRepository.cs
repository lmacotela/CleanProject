namespace AppointmentSearch.Domain.Doctors
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Doctor doctor);
    }
}