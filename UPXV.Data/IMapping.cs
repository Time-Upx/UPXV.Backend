using Microsoft.EntityFrameworkCore;
using UPXV.Models;

namespace UPXV.Data;

public interface IMapping<TModel> : IEntityTypeConfiguration<TModel> where TModel : class, IBaseModel;
