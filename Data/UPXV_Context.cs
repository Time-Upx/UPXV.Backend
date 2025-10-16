using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UPXV.Backend.Data.Mappings;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data;

public class UPXV_Context : DbContext
{
   public UPXV_Context (DbContextOptions<UPXV_Context> options) : base(options) {}
   public DbSet<Consumable> Consumables { get; set; }
   public DbSet<Patrimony> Patrimonies { get; set; }
   public DbSet<Status> Status { get; set; }
   public DbSet<Tag> Tags { get; set; }
   public DbSet<Unit> Units { get; set; }
   public DbSet<QRCode> QRCodes { get; set; }
   public DbSet<Intent> Intents { get; set; }
   protected override void OnModelCreating (ModelBuilder builder)
   {
      builder.AutoIncrementColumns();
      builder.ApplyConfiguration(new ConsumableMapping());
      builder.ApplyConfiguration(new PatrimonyMapping());
      builder.ApplyConfiguration(new StatusMapping());
      builder.ApplyConfiguration(new QRCodeMapping());
      builder.ApplyConfiguration(new IntentMapping());
      builder.ApplyConfiguration(new UnitMapping());
      builder.ApplyConfiguration(new TagMapping());
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
   public static bool TryFind<TEntity> (this DbContext context, out TEntity entity, params object?[]? keys) where TEntity : class
   {
      entity = context.Set<TEntity>().Find(keys)!;
      return entity is not null;
   }
   public static bool Exists<TEntity>(this DbContext context, params object?[]? keys) where TEntity : class
   {
      return context.Set<TEntity>().Find(keys) is not null;
   }
   public static bool ExistsOtherWithName<TEntity> (this DbContext context, string name) where TEntity : class, INamedEntity
   {
      return context.Set<TEntity>().Where(e => e.Name == name).Take(2).AsNoTracking().ToList().Count > 1;
   }
   public static bool Exists<TEntity> (this DbContext context, string name) where TEntity : class, INamedEntity
   {
      return context.Set<TEntity>().AsNoTracking().FirstOrDefault(e => e.Name == name) is not null;
   }
   public static bool Exists<TEntity> (this DbContext context, Expression<Func<TEntity, bool>> property) where TEntity : class
   {
      return context.Set<TEntity>().AsNoTracking().FirstOrDefault(property) is not null;
   }
}
