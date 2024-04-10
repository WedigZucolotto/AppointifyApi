using Appointify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointify.Infastructure.Mappings
{
    internal sealed class UserMapping : Mapping<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(u => u.Name)
               .HasColumnName("name")
               .IsRequired();

            builder.Property(u => u.CompleteName)
               .HasColumnName("complete_name")
               .IsRequired();

            builder.Property(u => u.Password)
               .HasColumnName("password")
               .IsRequired();

            builder.Property(u => u.CompanyId)
               .HasColumnName("company_id")
               .IsRequired();

            builder.HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(b => b.CompanyId);

            base.Configure(builder);
        }
    }
}
