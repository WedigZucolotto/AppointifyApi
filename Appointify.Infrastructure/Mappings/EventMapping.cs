using Appointify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointify.Infastructure.Mappings
{
    internal sealed class EventMapping : Mapping<Event>
    {
        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder.Property(e => e.Title)
               .HasColumnName("title")
               .IsRequired();

            builder.Property(e => e.Description)
               .HasColumnName("description");

            builder.Property(e => e.Schedule)
               .HasColumnName("schedule")
               .IsRequired();

            builder.Property(e => e.User)
               .HasColumnName("user_id")
               .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.UserId);

            base.Configure(builder);
        }
    }
}
