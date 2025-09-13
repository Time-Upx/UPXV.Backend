namespace UPXV.Services.Exceptions;

public class EntityNotFoundException<TEntity> : Exception
{
   public int? Nid { get; init; }
   public Type? EntityType { get; init; }
   public static string GetMessage(int? nid = null, Type? type = null)
   {
      string msg = "Entity ";
      if (type is not null) msg += $"of type [{type.Name}] ";
      if (nid is not null) msg += $"with nid [{nid}] ";
      msg += "not found";
      return msg;
   }

   public EntityNotFoundException () : base(GetMessage()) { }
   public EntityNotFoundException (string? message) : base(message) {}
   public EntityNotFoundException (string? message, Exception? innerException) : base(message, innerException) {}
   public EntityNotFoundException(int nid) : base(GetMessage(nid, typeof(TEntity)))
   {
      Nid = nid;
      EntityType = typeof(TEntity);
   }
}
