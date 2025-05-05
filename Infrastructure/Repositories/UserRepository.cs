using Domain.Models;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository {
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User> GetByEmail(string email) => await _context.User.FirstOrDefaultAsync(x => x.Email == email);
    
}
