using UPXV.Models;

namespace UPXV.DTOs;

public interface IDTO<TEntity> where TEntity : class, IEntityBase;
