using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class 
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _Dbset; 
    public BaseRepository(AppDbContext context )
    {
        _context = context;
        _Dbset = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _Dbset.ToListAsync();

    public async Task<T> GetByIdAsync(long id) => await _Dbset.FindAsync(id);
    public void Create(T entity) => _Dbset.Add(entity);
    public void Delete(T entity) => _Dbset.Remove(entity);
    public void Update(T entity) => _Dbset.Update(entity);
    public void SaveChange() 
    {
        _context.SaveChanges();
        _context.Dispose();
    }
}
