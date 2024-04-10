using Appointify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointify.Infastructure.Mappings
{
    internal sealed class CompanyMapping : Mapping<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");

            builder.Property(c => c.Name)
               .HasColumnName("name")
               .IsRequired();

            builder.Property(c => c.Interval)
               .HasColumnName("interval")
               .IsRequired();

            builder.HasMany(c => c.Users)
               .WithOne(u => u.Company);

            base.Configure(builder);
        }
    }
}
