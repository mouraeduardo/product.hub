using Domain.Models;
using Domain.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository {
    public ProductRepository(AppDbContext context) : base(context) { }
}
