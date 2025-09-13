using UPXV.Models;

namespace UPXV.DTOs;

public interface IListDTO<TEntity> : IDTO<TEntity> where TEntity : class, IEntityBase
{
   public IListDTO<TEntity> From (TEntity entity);
}
