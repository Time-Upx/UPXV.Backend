using Microsoft.EntityFrameworkCore;
using UPXV.Models;

namespace UPXV.Data;

public interface IMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntityBase;
