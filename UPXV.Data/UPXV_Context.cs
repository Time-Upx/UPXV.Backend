using Microsoft.EntityFrameworkCore;
using UPXV.Data.Mappings;
using UPXV.Models;

namespace UPXV.Data;

public class UPXV_Context : DbContext
{
   public UPXV_Context (DbContextOptions<UPXV_Context> options) : base(options) 
   {

   }
   public DbSet<Consumable> Consumables { get; set; }
   public DbSet<Patrimony> Patrimonies { get; set; }
   public DbSet<Status> Status { get; set; }
   public DbSet<Tag> Tags { get; set; }
   public DbSet<Unit> Units { get; set; }
   protected override void OnModelCreating (ModelBuilder builder)
   {
      builder.AutoIncrementColumns();
      builder.ApplyConfiguration(new ConsumableMapping());
      builder.ApplyConfiguration(new PatrimonyMapping());
      builder.ApplyConfiguration(new StatusMapping());
      builder.ApplyConfiguration(new TagMapping());
      builder.ApplyConfiguration(new UnitMapping());
   }
}
