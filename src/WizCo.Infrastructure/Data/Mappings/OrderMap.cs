using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizCo.Domain.Entities;
using WizCo.Domain.Enums;

namespace WizCo.Infrastructure.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.ClientName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion(s => s.ToString(), s => (StatusOrder)Enum.Parse(typeof(StatusOrder), s));

            builder.Property(o => o.TotalValue)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Visible)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);

            builder.HasQueryFilter(i => i.Visible);
        }
    }
}
