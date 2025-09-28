using System.Runtime.Serialization;
using UPXV.Data;
using UPXV.Models;

namespace UPXV.Services.Exceptions;

public class QueryReadingException<TEntity> : Exception
   where TEntity : class
{
   public Query<TEntity> Query { get; set; }
   public Type QueriedType { get; private set; } = typeof(TEntity);
   public QueryReadingException (Query<TEntity> query)
   {
      Query = query;
   }
   public QueryReadingException (string? message, Query<TEntity> query) : base(message)
   {
      Query = query;
   }
   public QueryReadingException ()
   {
   }

   public QueryReadingException (string? message) : base(message)
   {
   }

   public QueryReadingException (string? message, Exception? innerException) : base(message, innerException)
   {
   }

   protected QueryReadingException (SerializationInfo info, StreamingContext context) : base(info, context)
   {
   }
}
