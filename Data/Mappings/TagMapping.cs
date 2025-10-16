using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class TagMapping : IEntityTypeConfiguration<Tag>
{
   public void Configure (EntityTypeBuilder<Tag> builder)
   {
      builder.HasKey(t => t.Id);
      builder.HasIndex(t => t.Name).IsUnique();
   }
}
