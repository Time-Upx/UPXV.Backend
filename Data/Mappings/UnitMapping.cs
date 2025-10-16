using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class UnitMapping : IEntityTypeConfiguration<Unit>
{
   public void Configure(EntityTypeBuilder<Unit> builder)
   {
      builder.HasKey(u => u.Id);

      builder.HasIndex(u => u.Name).IsUnique();
      builder.HasIndex(u => u.Abbreviation).IsUnique();
   }
}
