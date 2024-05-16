using Appointify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointify.Infrastructure.Mappings
{
    internal sealed class EventMapping : Mapping<Event>
    {
        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("event");

            builder.Property(e => e.Title)
               .HasColumnName("title")
               .IsRequired();

            builder.Property(e => e.Description)
               .HasColumnName("description")
               .IsRequired();

            builder.Property(e => e.Date)
               .HasColumnName("date")
               .IsRequired();

            builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.UserId);

            builder.Property(e => e.ServiceId)
               .HasColumnName("service_id")
               .IsRequired();

            builder.HasOne(e => e.Service)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.ServiceId);

            base.Configure(builder);
        }
    }
}
