using AppointmentSearch.Domain.Appointments;
using AppointmentSearch.Domain.Doctors;
using AppointmentSearch.Domain.Shared;
using AppointmentSearch.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSearch.Infrastructure.Configurations;

internal sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("appointments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
            
        builder.OwnsOne(appointment => appointment.Price, priceBuilder=>
        {
            priceBuilder.Property(Currency => Currency.Currency)
            .HasConversion(currencyType=> currencyType.Type, code=> Currency.FromType(code!));
        });

        builder.OwnsOne(appointment => appointment.Period, periodBuilder=>
        {
            periodBuilder.Property(period => period.Start)
            .HasColumnName("start")
            .IsRequired();

            periodBuilder.Property(period => period.End)
            .HasColumnName("end")
            .IsRequired();
        });

        builder.HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(appointment => appointment.DoctorId);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(appointment => appointment.UserId);
    }
}