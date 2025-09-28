using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class PatrimonyMapping : IEntityTypeConfiguration<Patrimony>
{
   public void Configure (EntityTypeBuilder<Patrimony> builder)
   {
      builder.HasKey(p => p.Nid);
      builder.HasIndex(p => p.Tid).IsUnique();

      builder.HasMany(p => p.Tags)
         .WithMany();

      builder.HasOne(c => c.Status)
         .WithMany()
         .HasForeignKey(c => c.StatusNid);
   }
}
