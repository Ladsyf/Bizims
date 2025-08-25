using Bizims.Domain.Repositories;
using Bizims.Infrastructure.Contexts;

namespace Bizims.Infrastructure.Repositories;

internal class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly IDictionary<string, object> _repositoryCache;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _repositoryCache = new Dictionary<string, object>();
    }

    public IRepository<TEntityModel> Repository<TEntityModel>() where TEntityModel : class
    {
        var type = typeof(TEntityModel).Name;

        if (_repositoryCache.TryGetValue(type, out var repo))
        {
            if (repo is not IRepository<TEntityModel> cachedRepo)
                throw new Exception("Repository not found");

            return cachedRepo;
        }

        var newRepo = new Repository<TEntityModel>(_repositoryContext);

        _repositoryCache.Add(type, newRepo);

        return newRepo;
    }

    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
}