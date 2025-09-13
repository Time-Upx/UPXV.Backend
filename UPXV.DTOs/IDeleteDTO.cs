using UPXV.Models;

namespace UPXV.DTOs;

public interface IDeleteDTO<TEntity> : IDTO<TEntity> where TEntity : class, IEntityBase
{
   public int Nid { get; init; }
}
