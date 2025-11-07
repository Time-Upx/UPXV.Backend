using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class QRCodeMapping : IEntityTypeConfiguration<QRCode>
{
   public void Configure (EntityTypeBuilder<QRCode> builder)
   {
      builder.HasKey(qr => qr.Id);
      builder.Property(qr => qr.Id).HasValueGenerator<GuidGenerator>();
      builder.HasIndex(qr => qr.Name).IsUnique();

      builder.HasOne(qr => qr.Intent)
         .WithMany()
         .HasForeignKey(qr => qr.IntentId)
         .OnDelete(DeleteBehavior.NoAction);

      builder.HasMany(qr => qr.Arguments)
         .WithOne(a => a.QRCode)
         .HasForeignKey(a => a.QRCodeId)
         .OnDelete(DeleteBehavior.Cascade);

      builder.Ignore(qr => qr.HasExpired);
      builder.Ignore(qr => qr.HasReachedUsageLimit);
   }
}

public class GuidGenerator : ValueGenerator<string>
{
   public override bool GeneratesTemporaryValues => false;
   public override string Next (EntityEntry entry) => Guid.NewGuid().ToString();
}