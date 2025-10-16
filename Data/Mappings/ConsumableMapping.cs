using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class ConsumableMapping : IEntityTypeConfiguration<Consumable>
{
   public void Configure (EntityTypeBuilder<Consumable> builder)
   {
      builder.HasKey(c => c.Id);
      builder.HasIndex(c => c.Name).IsUnique();

      builder.HasMany(c => c.Tags)
         .WithMany();

      builder.HasOne(c => c.Unit)
         .WithMany()
         .HasForeignKey(c => c.UnitId);
   }
}
