using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class IntentMapping : IEntityTypeConfiguration<Intent>
{
   public void Configure (EntityTypeBuilder<Intent> builder)
   {
      builder.HasKey(i => i.Id);
      builder.HasIndex(i => i.Name).IsUnique();

      builder.HasMany(i => i.Parameters)
         .WithOne(p => p.Intent)
         .HasForeignKey(p => p.IntentId)
         .OnDelete(DeleteBehavior.Cascade);
   }
}
