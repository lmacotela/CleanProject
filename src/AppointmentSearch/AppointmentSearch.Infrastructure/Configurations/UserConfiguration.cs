using AppointmentSearch.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSearch.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.Name)
            .HasColumnName("name")
            .HasConversion(name => name!.Value, value => new Name(value))
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .HasConversion(lastName => lastName!.Value, value => new LastName(value))
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasColumnName("email")
            .HasConversion(email => email!.Value, value => new Domain.Users.Email(value))
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();
    }
}