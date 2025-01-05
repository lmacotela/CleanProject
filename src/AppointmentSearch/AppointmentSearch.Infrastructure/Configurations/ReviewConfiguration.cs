using AppointmentSearch.Domain.Appointments;
using AppointmentSearch.Domain.Doctors;
using AppointmentSearch.Domain.Reviews;
using AppointmentSearch.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSearch.Infrastructure.Configurations;
internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Rating)
        .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

        builder.Property(review => review.Comentary)
            .HasColumnName("comment")
            .HasMaxLength(200)
            .HasConversion(comment => comment!.Value, value => new Comment(value))
            .IsRequired();

        builder.HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(review => review.DoctorId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(review => review.UserId);

        builder.HasOne<Appointment>()
        .WithMany()
        .HasForeignKey(review => review.AppointmentId);
    }
}