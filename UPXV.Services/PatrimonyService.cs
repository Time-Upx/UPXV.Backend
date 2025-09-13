using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class PatrimonyService (PatrimonyRepository repository) : ServiceBase<Patrimony>(repository)
{
   protected new PatrimonyRepository _repository => (PatrimonyRepository) _repository;
}