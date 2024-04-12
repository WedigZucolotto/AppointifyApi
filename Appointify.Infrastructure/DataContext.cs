using Appointify.Domain.Entities;
using Appointify.Domain;
using Appointify.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Appointify.Infrastructure
{
    public class DataContext : DbContext, IUnitOfWork
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConnectionOptions _connectionOptions;

        public DataContext(
            DbContextOptions<DataContext> options,
            //IHttpContextAccessor httpContextAccessor,
            IOptions<ConnectionOptions> connectionOptions)
            : base(options)
        {
            //_httpContextAccessor = httpContextAccessor;
            _connectionOptions = connectionOptions.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionOptions.DefaultConnection);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }


        public virtual async Task CommitAsync()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified)
                .Where(e => e.Entity is Entity);

            //tentar pegar do Ihttpcontext
            //var userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var userIdConverted = userId != null ? Guid.Parse(userId) : Guid.Empty;

            foreach (var entry in modifiedEntries)
            {
                var entity = (Entity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    //entity.CreatedBy = userIdConverted;
                }

                entity.ModifiedAt = DateTime.UtcNow;
                //entity.ModifiedBy = userIdConverted;

                //ConvertDatesToUtc(entity);
            }

            await SaveChangesAsync();
        }

        //private void ConvertDatesToUtc(Entity entity)
        //{
        //    var properties = entity.GetType().GetProperties()
        //        .Where(prop => prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?));

        //    foreach (var prop in properties)
        //    {
        //        var value = (DateTime?)prop.GetValue(entity);

        //        if (value != null)
        //        {
        //            prop.SetValue(entity, value.Value.ToUniversalTime());
        //        }
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new EventMapping());
            modelBuilder.ApplyConfiguration(new PlanMapping());
            modelBuilder.ApplyConfiguration(new ServiceMapping());
        }
    }
}
