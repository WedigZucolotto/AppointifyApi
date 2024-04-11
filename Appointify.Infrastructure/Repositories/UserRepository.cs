using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;

namespace Appointify.Infastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
