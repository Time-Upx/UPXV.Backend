using UPXV.Models;

namespace UPXV.DTOs;

public interface IUpdateDTO<TEntity> : IDTO<TEntity> where TEntity : class, IEntityBase 
{ 
   public int Nid { get; set; }
   public void Update(ref TEntity entity);
}