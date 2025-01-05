using AppointmentSearch.Domain.Appointments;
using AppointmentSearch.Domain.Doctors;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSearch.Infrastructure.Repositories;

internal sealed class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    private static readonly Status[] ActiveAppointmentStatuses 
    = { Status.Reserved, Status.Confirmed, Status.Completed };
    public AppointmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsOverlappingAsync(Doctor doctor, DateRange period, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Appointment>()
            .AnyAsync(appointment => appointment.DoctorId == doctor.Id && 
            appointment.Period.Start<= period.End && appointment.Period.End >= period.Start &&
            ActiveAppointmentStatuses.Contains(appointment.Status), cancellationToken);
    }
}