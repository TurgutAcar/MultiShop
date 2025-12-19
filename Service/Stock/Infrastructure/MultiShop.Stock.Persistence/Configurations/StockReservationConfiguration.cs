using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiShop.Services.Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Persistence.Configurations
{
    public class StockReservationConfiguration : IEntityTypeConfiguration<StockReservation>
    {
        public void Configure(EntityTypeBuilder<StockReservation> entity)
        {
            entity.ToTable("StockReservation");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ProductId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Quantity)
                  .IsRequired();

            entity.Property(e => e.CartId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.ExpiresAt)
                  .HasColumnType("datetime")
                  .IsRequired();

            entity.Property(e => e.Status)
                  .HasConversion<int>() // Enum -> int
                  .IsRequired();



        }
    }


}
