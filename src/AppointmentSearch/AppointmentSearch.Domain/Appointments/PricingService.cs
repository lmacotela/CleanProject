using System.Diagnostics;
using AppointmentSearch.Domain.Doctors;
using AppointmentSearch.Domain.Shared;

namespace AppointmentSearch.Domain.Appointments
{
    public class PricingService
    {
        public PricingDetail CalculatePrice(Doctor doctor, DateRange period)
        {
            var Currency = doctor.Salary!.Currency;
            var priceForPeriod =new Money(
                period.LengthInDays * doctor.Salary.Amount, Currency);
            decimal porcentageChange=0;
            foreach(var speciality in doctor.Specialities)
            {
                porcentageChange += speciality switch
                {
                    Speciality.Allergist => 0.01m,
                    Speciality.Anesthesiologist => 0.02m,
                    Speciality.Cardiologist => 0.03m,
                    Speciality.Dermatologist => 0.04m,
                    Speciality.Endocrinologist => 0.05m,
                    Speciality.Gastroenterologist => 0.06m,
                    Speciality.Hematologist => 0.07m,
                    Speciality.Immunologist => 0.08m,
                    _ => 0
                };
            }

            var AdicionalCharge= Money.Zero(Currency);
            if(porcentageChange>0)
            {
                AdicionalCharge = new Money(priceForPeriod.Amount * porcentageChange, Currency);
            }
            
            var TotalPrice= Money.Zero();
            TotalPrice+= priceForPeriod;
            TotalPrice+= AdicionalCharge;

            if(!doctor.Salary.IsZero())
            {
                TotalPrice+= doctor.Salary;
            }

            return new PricingDetail(priceForPeriod,doctor.Salary,AdicionalCharge,TotalPrice);
        }
    }
}