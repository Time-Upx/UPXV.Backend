using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class TagMapping : IEntityTypeConfiguration<Tag>
{
   public void Configure (EntityTypeBuilder<Tag> builder)
   {
      builder.HasKey(t => t.Nid);
      builder.HasIndex(t => t.Tid).IsUnique();
   }
}
