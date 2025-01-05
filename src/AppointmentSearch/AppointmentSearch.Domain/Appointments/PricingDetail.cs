using AppointmentSearch.Domain.Shared;

namespace AppointmentSearch.Domain.Appointments
{
    public record PricingDetail(
        Money PriceForPeriod,
        Money Salary,
        Money AdicionalCharge,
        Money TotalPrice
        );
}