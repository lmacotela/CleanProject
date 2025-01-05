using AppointmentSearch.Domain.Doctors;

namespace AppointmentSearch.Infrastructure.Repositories;

internal sealed class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    public DoctorRepository(ApplicationDbContext context) : base(context)
    {
    }
}
