using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
using Appointify.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public Task<User?> GetByNameAsync(string name)
        {
            return Query
                .FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}
