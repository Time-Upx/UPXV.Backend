using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class ItemMapping : IMapping<Item>
{
   public void Configure (EntityTypeBuilder<Item> builder)
   {
      builder.HasKey(i => i.Nid);
      builder.HasAlternateKey(i => i.Tid);

      builder.HasMany(i => i.Tags)
         .WithMany();
   }
}
