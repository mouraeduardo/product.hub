using Domain.Models;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel 
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _Dbset;
    private IDbContextTransaction _transaction;

    public BaseRepository(AppDbContext context )
    {
        _context = context;
        _Dbset = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _Dbset.ToListAsync();

    public async Task<T> GetByIdAsync(long id) => await _Dbset.FindAsync(id);
    public void Create(T entity) => _Dbset.Add(entity);
    public void Delete(T entity) 
    {
        entity.DeletionDate = DateTime.UtcNow;
        _Dbset.Update(entity);
    }
    public void Update(T entity) => _Dbset.Update(entity);
    public void SaveChange() 
    {
        _context.SaveChanges();
    }

    public void BeginTransaction() 
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit() 
    {
        try 
        {
            _context.SaveChanges();
            _transaction.Commit();
        }
        catch 
        {
            _transaction.Rollback();
            throw;
        }
        finally 
        {
            _transaction.Dispose();
        }
    }

    public void Rollback() 
    {
        _transaction.Rollback();
        _transaction.Dispose();
    }

}
