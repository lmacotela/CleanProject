using AppointmentSearch.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSearch.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
    protected readonly ApplicationDbContext Context;

    protected Repository(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(user=> user.Id== id, cancellationToken);
    }

    public void Add(T entity)
    {
        Context.Add(entity);
    }
}