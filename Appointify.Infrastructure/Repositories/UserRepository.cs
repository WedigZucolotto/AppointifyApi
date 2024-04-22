﻿using Appointify.Domain.Entities;
using Appointify.Domain.Repositories;
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
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}
