using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class ConsumableMapping : IMapping<Consumable>
{
   public void Configure (EntityTypeBuilder<Consumable> builder)
   {
      builder.HasKey(c => c.Nid);
      builder.HasAlternateKey(c => c.Tid);

      builder.HasOne(c => c.Unit)
         .WithMany()
         .HasForeignKey(c => c.UnitNid);

      builder.HasOne(c => c.Item)
         .WithMany()
         .HasForeignKey(c => c.ItemNid);
   }
}
