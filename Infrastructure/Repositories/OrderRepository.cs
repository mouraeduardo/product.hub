using Domain.Models;
using Domain.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository {
    public OrderRepository(AppDbContext context) : base(context) {
    }
}
