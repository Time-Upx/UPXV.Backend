using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class StatusMapping : IMapping<Status>
{
   public void Configure (EntityTypeBuilder<Status> builder)
   {
      builder.HasKey(s => s.Nid);
      builder.HasAlternateKey(s => s.Tid);
   }
}
