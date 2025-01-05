using AppointmentSearch.Application.Abstractions.Behaviors;
using AppointmentSearch.Domain.Appointments;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentSearch.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration => 
            {
                configuration.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly);
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependecyInjection).Assembly);
            services.AddTransient<PricingService>();
            return services;
        }
    }
}