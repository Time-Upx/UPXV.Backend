using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class TagMapping : IMapping<Tag>
{
   public void Configure (EntityTypeBuilder<Tag> builder)
   {
      builder.HasKey(t => t.Nid);
      builder.HasAlternateKey(t => t.Tid);
   }
}
