using System.Linq.Expressions;

namespace OrobaSoft.Shared.Interfaces;
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties);
    Task<T> GetById(int id, params Expression<Func<T, object>>[] includeProperties);
    Task Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
