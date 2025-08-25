
namespace Bizims.Domain.Repositories;

public interface IRepositoryManager
{
    IRepository<TEntityModel> Repository<TEntityModel>() where TEntityModel : class;
    Task SaveAsync();
}