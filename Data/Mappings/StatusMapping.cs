using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class StatusMapping : IEntityTypeConfiguration<Status>
{
   public void Configure (EntityTypeBuilder<Status> builder)
   {
      builder.HasKey(s => s.Id);
      builder.HasIndex(s => s.Name).IsUnique();
   }
}
