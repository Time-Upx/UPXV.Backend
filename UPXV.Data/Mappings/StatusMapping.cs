using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class StatusMapping : IEntityTypeConfiguration<Status>
{
   public void Configure (EntityTypeBuilder<Status> builder)
   {
      builder.HasKey(s => s.Nid);
      builder.HasIndex(s => s.Tid).IsUnique();
   }
}
