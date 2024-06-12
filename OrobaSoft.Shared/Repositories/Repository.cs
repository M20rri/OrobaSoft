using Microsoft.EntityFrameworkCore;
using OrobaSoft.API.Interfaces;
using OrobaSoft.Shared.Context;
using OrobaSoft.Shared.Interfaces;
using System.Linq.Expressions;

namespace OrobaSoft.Shared.Repositories;
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly InvoiceDbContext _context;
    public Repository(InvoiceDbContext context)
    {
        _context = context;
    }

    //public async Task<IEnumerable<T>> GetAll()
    //{
    //    return await _context.Set<T>().ToListAsync();
    //} 

    public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetById(int id, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        // Assuming the primary key property is named "Id"
        return await query.FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);

    }

    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
