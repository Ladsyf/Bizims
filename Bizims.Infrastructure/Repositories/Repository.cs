using Bizims.Domain.Repositories;
using Bizims.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bizims.Infrastructure.Repositories;

internal class Repository<TEntityModel> : IRepository<TEntityModel> where TEntityModel : class
{
    private readonly DbSet<TEntityModel> _dbSet;

    public Repository(RepositoryContext repositoryContext)
    {
        _dbSet = repositoryContext.Set<TEntityModel>();
    }

    public async Task InsertAsync(TEntityModel entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<TEntityModel?> FindByAsync(Expression<Func<TEntityModel, bool>> condition)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(condition);
    }

    public async Task<TEntityModel?> FindByAsync(Expression<Func<TEntityModel, bool>> condition, Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> extendQuery)
    {
        var queryableEntities = _dbSet
            .AsNoTracking();

        return await extendQuery(queryableEntities)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<IReadOnlyList<TEntityModel>> FindAllByAsync(Expression<Func<TEntityModel, bool>> condition)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(condition)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<TEntityModel>> FindAllByAsync(Expression<Func<TEntityModel, bool>> condition, Func<IQueryable<TEntityModel>, IQueryable<TEntityModel>> extendQuery)
    {
        var queryableEntities = _dbSet
            .AsNoTracking();

        return await extendQuery(queryableEntities)
            .Where(condition)
            .ToListAsync();
    }

    public async Task UpdateAsync(Expression<Func<TEntityModel, bool>> condition, Action<TEntityModel> updateAction)
    { 
        var entity = await _dbSet.FirstOrDefaultAsync(condition);

        if (entity == null) return;

        updateAction(entity);
    }

    public async Task DeleteAsync(Expression<Func<TEntityModel, bool>> condition)
    { 
        var entities = await _dbSet.Where(condition).ToListAsync();

        _dbSet.RemoveRange(entities);
    }
}