
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
using MultiShop.Services.Stock.Domain.Entities;

namespace MultiShop.Services.Stock.Persistence.Configurations
{
    public class StockTransactionConfiguration : IEntityTypeConfiguration<Domain.Entities.StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> entity)
        {
            entity.ToTable("StockTransaction");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ProductId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Quantity)
                  .IsRequired();

          
            entity.Property(p => p.Type)
                .HasConversion(type => type.Value, value => StockTransactionType.FromValue(value))
                .IsRequired();
            entity.Property(e => e.ReferenceId)
                  .IsRequired();

            entity.Property(e => e.CreatedAt)
                  .HasColumnType("datetime")
                  .IsRequired();



        }
    }
}
