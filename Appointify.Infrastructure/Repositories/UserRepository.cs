using Appointify.Domain.Entities;
using Appointify.Domain.Entities.Enums;
using Appointify.Domain.Repositories;
using Appointify.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Appointify.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public override Task<List<User>> GetAllAsync() =>
            Query
                .Include(u => u.Company)
                .ToListAsync();

        public override Task<User?> GetByIdAsync(Guid id) =>
            Query
                .Include(u => u.Events)
                .FirstOrDefaultAsync(u => u.Id == id);

        public Task<User?> GetByNameAsync(string name) =>
            Query.FirstOrDefaultAsync(u => u.Name == name);

        public Task<User?> GetByIdToDeleteAsync(Guid id) =>
            Query
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Id == id);

        public Task<List<User>> GetFilteredAsync(Guid? companyId, string? name, string? completeName, UserType? type) =>
           Query
               .Include(u => u.Company)
               .ConditionalFilter(c => c.CompanyId == companyId, companyId.HasValue && companyId != Guid.Empty)
               .ConditionalFilter(c => c.Name.ToLower().Contains(name.ToLower()), !string.IsNullOrEmpty(name))
               .ConditionalFilter(c => c.CompleteName.ToLower().Contains(completeName.ToLower()), !string.IsNullOrEmpty(completeName))
               .ConditionalFilter(c => c.Type.Equals(type), type.HasValue)
               .ToListAsync();
    }
}
