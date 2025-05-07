using Domain.Models;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository {
    public OrderRepository(AppDbContext context) : base(context) {
    }

    public async Task<IEnumerable<Order>> GetWithOrderProductsAsync() => await _context.Order.Include(o => o.OrderProductList).ToListAsync();
   
    public async Task<Order> GetWithOrderProductsByIdAsync(long id) => await _context.Order.Include(o => o.OrderProductList).FirstOrDefaultAsync(o => o.Id == id);
}
