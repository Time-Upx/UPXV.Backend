using UPXV.Data;
using UPXV.Dto;
using UPXV.Models;

namespace UPXV.Services;

public class ServiceBase<TModel> where TModel : class, IBaseModel
{
   protected RepositoryBase<TModel> _repository;

   public ServiceBase (RepositoryBase<TModel> repository) => _repository = repository;
}
