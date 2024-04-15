using Appointify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointify.Infrastructure.Mappings
{
    internal sealed class CompanyMapping : Mapping<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("company");

            builder.Property(c => c.Name)
               .HasColumnName("name")
               .IsRequired();

            builder.HasMany(c => c.Users)
               .WithOne(u => u.Company);

            builder.Property(c => c.PlanId)
               .HasColumnName("plan_id")
               .IsRequired();

            builder.HasOne(c => c.Plan)
                .WithMany(p => p.Companies)
                .HasForeignKey(c => c.PlanId);

            builder.Property(c => c.Open)
               .HasColumnName("open")
               .IsRequired();

            builder.Property(c => c.Close)
               .HasColumnName("close")
               .IsRequired();

            builder.HasMany(c => c.Services)
               .WithOne(s => s.Company);

            base.Configure(builder);
        }
    }
}
