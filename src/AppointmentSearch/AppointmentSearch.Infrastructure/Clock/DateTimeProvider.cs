using AppointmentSearch.Application.Abstractions.Clock;

namespace AppointmentSearch.Infrastructure.Clock;

internal sealed class DatetimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}