using AppointmentSearch.Application.Abstractions.Email;

namespace AppointmentSearch.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.Email to, string subject, string body, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

}