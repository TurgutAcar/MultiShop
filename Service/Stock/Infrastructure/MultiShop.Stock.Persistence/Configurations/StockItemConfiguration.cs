using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiShop.Services.Stock.Domain.Entities;
namespace MultiShop.Services.Stock.Persistence.Configurations
{
    public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> entity)
        {
            entity.ToTable("StockItem");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ProductId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.TotalQuantity)
                  .IsRequired();

            entity.Property(e => e.ReservedQuantity)
                  .HasDefaultValue(0);

            entity.Property(e => e.UpdatedAt)
                  .HasColumnType("datetime")
                  .IsRequired();







        }
    }
}
