using Appointify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointify.Infrastructure.Mappings
{
    internal sealed class ServiceMapping : Mapping<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("service");

            builder.Property(s => s.Name)
               .HasColumnName("name")
               .IsRequired();

            builder.Property(s => s.Interval)
               .HasColumnName("interval")
               .IsRequired();

            builder.Property(s => s.CompanyId)
               .HasColumnName("company_id")
               .IsRequired();

            builder.HasOne(s => s.Company)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CompanyId);

            builder.HasMany(s => s.Events)
                .WithOne(e => e.Service);

            base.Configure(builder);
        }
    }
}
