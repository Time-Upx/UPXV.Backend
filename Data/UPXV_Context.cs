using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Data.Mappings;

namespace UPXV.Backend.Data;

public class UPXV_Context : DbContext
{
   public UPXV_Context (DbContextOptions<UPXV_Context> options) : base(options) {}
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

public static class ContextExtensions
{
   public static void Load<TEntity, TValue> (this DbContext context, TEntity entity, Expression<Func<TEntity, TValue?>> property) 
      where TEntity : class
      where TValue : class
   {
      if (entity == null) return;
      context.Entry(entity).Reference(property).Load();
   }

   public static void LoadCollection<TEntity, TValue> (this DbContext context, TEntity entity, Expression<Func<TEntity, IEnumerable<TValue>>> property)
      where TEntity : class
      where TValue : class
   {
      if (entity == null) return;
      context.Entry(entity).Collection(property).Load();
   }

   public static void LoadRequirements<TEntity> (this DbContext context, TEntity entity) 
      where TEntity : class, IHasRequirements
   {
      entity.LoadRequirements(context);
   }

   public static void LoadRequirements (this DbContext context, object entity) {}
}
