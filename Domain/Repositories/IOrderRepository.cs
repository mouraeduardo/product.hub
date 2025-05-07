using Domain.Models;

namespace Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order> 
{
    public Task<IEnumerable<Order>> GetWithOrderProductsAsync();
    public Task<Order> GetWithOrderProductsByIdAsync(long id);

}
