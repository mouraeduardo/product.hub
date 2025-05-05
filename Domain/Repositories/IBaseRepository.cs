using System.Threading.Tasks;

namespace Domain.Repositories; 
public interface IBaseRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> GetByIdAsync(long id);
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public void SaveChange();
}
