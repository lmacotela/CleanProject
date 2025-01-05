using AppointmentSearch.Domain.Abstractions;
using AppointmentSearch.Domain.Shared;

namespace AppointmentSearch.Domain.Doctors
{
    public sealed class Doctor : Entity
    {
        private Doctor() { }

        public Doctor(
            Guid id,
            Name? name,
            LastName? lastName,
            Address? address,
            Money? salary,
            DateTime? lastAppointmentDate,
            List<Speciality> specialities)
        : base(id)
        {
            Name = name;
            LastName = lastName;
            Address = address;
            Salary = salary;
            LastAppointmentDate = lastAppointmentDate;
            Specialities = specialities;
        }
        public Name? Name { get; private set; }
        public LastName? LastName { get; private set; }
        public Address? Address { get; private set; }
        public Money? Salary { get; private set; }
        public DateTime? LastAppointmentDate { get; internal set; }
        public List<Speciality> Specialities { get; private set; } = new();
    }
}