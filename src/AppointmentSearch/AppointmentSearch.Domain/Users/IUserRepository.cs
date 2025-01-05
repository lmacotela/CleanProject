namespace AppointmentSearch.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(User user) ;
    }
}