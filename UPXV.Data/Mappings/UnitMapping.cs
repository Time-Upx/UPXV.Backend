using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class UnitMapping : IMapping<Unit>
{
   public void Configure(EntityTypeBuilder<Unit> builder)
   {
      builder.HasKey(u => u.Nid);
      builder.HasAlternateKey(u => u.Tid);
   }
}
