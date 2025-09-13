using UPXV.Models;

namespace UPXV.DTOs;

public interface IDetailDTO<TEntity> : IDTO<TEntity> where TEntity : class, IEntityBase
{
   public IDetailDTO<TEntity> From (TEntity entity);
}
