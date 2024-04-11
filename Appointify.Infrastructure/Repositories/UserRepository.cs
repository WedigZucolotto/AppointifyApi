using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
using Appointify.Infrastructure;

namespace Appointify.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
