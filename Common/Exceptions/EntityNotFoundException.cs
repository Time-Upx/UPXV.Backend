namespace UPXV.Backend.Common.Exceptions;

public class EntityNotFoundException<TEntity> : Exception
{
   public int? Id { get; init; }
   public Type? EntityType { get; init; }
   public static string GetMessage(int? id = null, Type? type = null)
   {
      string msg = "Entity ";
      if (type is not null) msg += $"of type [{type.Name}] ";
      if (id is not null) msg += $"with id [{id}] ";
      msg += "not found";
      return msg;
   }

   public EntityNotFoundException () : base(GetMessage()) { }
   public EntityNotFoundException (string? message) : base(message) {}
   public EntityNotFoundException (string? message, Exception? innerException) : base(message, innerException) {}
   public EntityNotFoundException(int id) : base(GetMessage(id, typeof(TEntity)))
   {
      Id = id;
      EntityType = typeof(TEntity);
   }
}
