namespace Bizims.Domain.Repositories;

public interface IRepository<TEntityModel>
{
    Task DeleteAsync(System.Linq.Expressions.Expression<Func<TEntityModel, bool>> condition);
    Task<IReadOnlyList<TEntityModel>> FindAllByAsync(System.Linq.Expressions.Expression<Func<TEntityModel, bool>> condition);
    Task<TEntityModel?> FindByAsync(System.Linq.Expressions.Expression<Func<TEntityModel, bool>> condition);
    Task InsertAsync(TEntityModel entity);
    Task UpdateAsync(System.Linq.Expressions.Expression<Func<TEntityModel, bool>> condition, Action<TEntityModel> updateAction);
}