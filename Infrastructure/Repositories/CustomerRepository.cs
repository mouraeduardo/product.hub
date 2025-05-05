using Domain.Models;
using Domain.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository {
    public CustomerRepository(AppDbContext context) : base(context) 
    {
    }
}
