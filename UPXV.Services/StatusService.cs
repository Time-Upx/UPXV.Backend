using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class StatusService (StatusRepository repository) : ServiceBase<Status>(repository)
{
   protected new StatusRepository _repository => (StatusRepository) _repository;
}
