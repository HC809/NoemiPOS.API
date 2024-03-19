using Microsoft.EntityFrameworkCore;
using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Infraestructure.Repositories;

internal abstract class Repository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _dbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


    public void Add(T entity) => _dbContext.Add(entity);
}
