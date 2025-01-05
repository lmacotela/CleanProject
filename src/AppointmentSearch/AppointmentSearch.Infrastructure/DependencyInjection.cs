using AppointmentSearch.Application.Abstractions.Clock;
using AppointmentSearch.Application.Abstractions.Data;
using AppointmentSearch.Application.Abstractions.Email;
using AppointmentSearch.Domain.Abstractions;
using AppointmentSearch.Domain.Appointments;
using AppointmentSearch.Domain.Doctors;
using AppointmentSearch.Domain.Users;
using AppointmentSearch.Infrastructure.Clock;
using AppointmentSearch.Infrastructure.Data;
using AppointmentSearch.Infrastructure.Email;
using AppointmentSearch.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentSearch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DatetimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IDoctorRepository), typeof(DoctorRepository));
        services.AddScoped(typeof(IAppointmentRepository), typeof(AppointmentRepository));

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISQLConnectionFactory>( _ =>
        new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        return services;
    }
}