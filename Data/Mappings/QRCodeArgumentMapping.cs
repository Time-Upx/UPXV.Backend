using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class QRCodeArgumentMapping : IEntityTypeConfiguration<QRCodeArgument>
{
   public void Configure (EntityTypeBuilder<QRCodeArgument> builder)
   {
      builder.HasKey(qa => qa.Id);

      builder.HasOne(qa => qa.QRCode)
         .WithMany(qr => qr.Arguments)
         .HasForeignKey(qa => qa.QRCodeId);
   }
}
