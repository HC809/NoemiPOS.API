using MediatR;
using Microsoft.EntityFrameworkCore;
using NoemiPOS.Application.Exceptions;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Users;
using NoemiPOS.Infraestructure.Multinenacy;
using System.Data;
using System.Linq.Expressions;

namespace NoemiPOS.Infraestructure;
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher, ICurrentUserService currentUserService) : base(options)
    {
        _publisher = publisher;
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseTenantEntity).IsAssignableFrom(entityType.ClrType) && entityType.ClrType != typeof(User))
            {
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(
                    CreateBusinessFilter(entityType.ClrType));
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    private LambdaExpression CreateBusinessFilter(Type type)
    {
        var parameter = Expression.Parameter(type, "e");
        var property = Expression.Property(parameter, nameof(BaseTenantEntity.BusinessId));
        var constant = Expression.Constant(_currentUserService.BusinessId);
        var equality = Expression.Equal(property, constant);
        var lambda = Expression.Lambda(equality, parameter);

        return lambda;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseTenantEntity mustHaveBussinessEntity)
                {
                    mustHaveBussinessEntity.BusinessId = _currentUserService.BusinessId;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred", ex);
        }
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker.Entries<Entity>().Select(entry => entry.Entity).SelectMany(entity =>
        {
            var domainEvents = entity.GetDomainEvents();
            entity.ClearDomainEvents();
            return domainEvents;
        }).ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
