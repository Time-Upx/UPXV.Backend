using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class PatrimonyMapping : IMapping<Patrimony>
{
   public void Configure (EntityTypeBuilder<Patrimony> builder)
   {
      builder.HasKey(p => p.Nid);
      builder.HasAlternateKey(p => p.Tid);

      builder.HasOne(c => c.Status)
         .WithMany()
         .HasForeignKey(c => c.StatusNid);

      builder.HasOne(c => c.Item)
         .WithMany()
         .HasForeignKey(c => c.ItemNid);
   }
}
