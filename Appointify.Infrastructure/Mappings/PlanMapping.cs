using Appointify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointify.Infrastructure.Mappings
{
    internal sealed class PlanMapping : Mapping<Plan>
    {
        public override void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("plan");

            builder.Property(p => p.Name)
               .HasColumnName("name")
               .IsRequired();

            builder.Property(p => p.ShowExtraFields)
               .HasColumnName("show_extra_fields")
               .IsRequired();

            builder.HasMany(p => p.Companies)
                .WithOne(c => c.Plan);

            base.Configure(builder);
        }
    }
}
