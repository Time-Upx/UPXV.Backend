using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class IntentParameterMapping : IEntityTypeConfiguration<IntentParameter>
{
   public void Configure (EntityTypeBuilder<IntentParameter> builder)
   {
      builder.HasKey(ip => ip.Id);

      builder.ToTable("IntentParameters");
   }
}
