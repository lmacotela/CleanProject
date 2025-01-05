using AppointmentSearch.Domain.Doctors;
using AppointmentSearch.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSearch.Infrastructure.Configurations;
internal sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("doctors");

        builder.HasKey(d => d.Id);

        builder.OwnsOne(doctor => doctor.Address);

        builder.Property(d => d.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(d => d.Name)
            .HasColumnName("name")
            .HasConversion(name=>name!.Value, value=>new Name(value))
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(d => d.LastName)
            .HasColumnName("last_name")
            .HasConversion(LastName=>LastName!.Value, value=>new LastName(value))
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(doctor => doctor.Salary, salaryBuilder=>
        {
            salaryBuilder.Property(currency => currency.Currency)
            .HasConversion(currencyType => currencyType.Type, code => Currency.FromType(code!));
        });
       builder.Property<uint>("Version").IsRowVersion();
    }
}
