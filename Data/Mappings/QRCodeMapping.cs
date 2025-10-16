using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class QRCodeMapping : IEntityTypeConfiguration<QRCode>
{
   public void Configure (EntityTypeBuilder<QRCode> builder)
   {
      builder.HasKey(qr => qr.Id);
      builder.HasIndex(qr => qr.Name).IsUnique();

      builder.HasOne(qr => qr.Intent)
         .WithMany()
         .HasForeignKey(qr => qr.IntentId);

      builder.Ignore(qr => qr.HasExpired);
      builder.Ignore(qr => qr.HasReachedUsageLimit);
   }
}
