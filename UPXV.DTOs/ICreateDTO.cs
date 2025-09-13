using UPXV.Models;

namespace UPXV.DTOs;

public interface ICreateDTO<TEntity> : IDTO<TEntity> where TEntity : class, IEntityBase
{
   public TEntity BuildEntity();
}
