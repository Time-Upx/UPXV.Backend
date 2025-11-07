using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Mappings;

public class PatrimonyMapping : IEntityTypeConfiguration<Patrimony>
{
   public void Configure (EntityTypeBuilder<Patrimony> builder)
   {
      builder.HasKey(p => p.Id);
      builder.HasIndex(p => p.Name).IsUnique();

      builder.HasMany(p => p.Tags)
         .WithMany()
         .UsingEntity<Dictionary<string, object>>(
            "PatrimoniesTags",
            r => r.HasOne<Tag>().WithMany()
                .HasForeignKey("TagId")
                .OnDelete(DeleteBehavior.Restrict),
            l => l.HasOne<Patrimony>().WithMany()
                .HasForeignKey("ConsumableId")
                .OnDelete(DeleteBehavior.Cascade));

      builder.HasOne(c => c.Status)
         .WithMany()
         .HasForeignKey(c => c.StatusId)
         .OnDelete(DeleteBehavior.NoAction);
   }
}
