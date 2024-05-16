using Appointify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointify.Infrastructure.Mappings
{
    internal sealed class UserMapping : Mapping<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(u => u.Name)
               .HasColumnName("name")
               .IsRequired();

            builder.Property(u => u.CompleteName)
               .HasColumnName("complete_name")
               .IsRequired();

            builder.Property(u => u.Password)
               .HasColumnName("password")
               .IsRequired();

            builder.Property(u => u.IsOwner)
              .HasColumnName("is_owner")
              .IsRequired();

            builder.Property(u => u.CompanyId)
               .HasColumnName("company_id")
               .IsRequired();

            builder.HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(b => b.CompanyId);

            builder.HasMany(u => u.Events)
                .WithOne(e => e.User);

            builder.HasMany(u => u.Permissions)
                .WithOne(p => p.User);

            base.Configure(builder);
        }
    }
}
