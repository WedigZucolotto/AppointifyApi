using Appointify.Domain.Entities;
using Appointify.Domain;
using Appointify.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Appointify.Domain.Authentication;

namespace Appointify.Infrastructure
{
    public class DataContext : DbContext, IUnitOfWork
    {
        private readonly IHttpContext _httpContext;

        public DataContext(
            DbContextOptions<DataContext> options,
            IHttpContext httpContext)
            : base(options)
        {
            _httpContext = httpContext;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            optionsBuilder.UseNpgsql(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public virtual async Task CommitAsync()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified)
                .Where(e => e.Entity is Entity);

            foreach (var entry in modifiedEntries)
            {
                var entity = (Entity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = _httpContext.GetUserId();
                }

                entity.ModifiedAt = DateTime.UtcNow;
                entity.ModifiedBy = _httpContext.GetUserId();
            }

            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new EventMapping());
            modelBuilder.ApplyConfiguration(new ServiceMapping());
            modelBuilder.ApplyConfiguration(new PermissionMapping());
            modelBuilder.ApplyConfiguration(new PlanMapping());
        }
    }
}
