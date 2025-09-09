using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class UnitService(RepositoryBase<Unit> repository) : ServiceBase<Unit> (repository)
{
   protected UnitRepository Repository => (UnitRepository) _repository;
}
