using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Appointify.Domain.Entities;

namespace Appointify.Infrastructure.Mappings
{
    internal sealed class PermissionMapping : Mapping<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permission");

            builder.Property(p => p.Name)
               .HasColumnName("name")
               .IsRequired();

            builder.Property(p => p.UserId)
               .HasColumnName("user_id")
               .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(u => u.Permissions)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
