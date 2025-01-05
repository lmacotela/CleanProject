namespace AppointmentSearch.Application.Abstractions.Email;
public interface IEmailService
{
    Task SendAsync(Domain.Users.Email to, string subject, string body, CancellationToken cancellationToken = default);
}
