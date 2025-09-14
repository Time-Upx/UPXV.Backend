using FluentValidation;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class StatusService : ServiceBase<Status>
{
   private StatusRepository _repository => (StatusRepository) _repositoryBase;
   public StatusService (StatusRepository repository, IValidator<Status> validator) : base(repository, validator)
   {
   }

}
