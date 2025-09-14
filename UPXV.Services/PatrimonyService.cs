using FluentValidation;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class PatrimonyService : ServiceBase<Patrimony>
{
   private PatrimonyRepository _repository => (PatrimonyRepository) _repositoryBase;
   public PatrimonyService (PatrimonyRepository repository, IValidator<Patrimony> validator) : base(repository, validator)
   {
   }
}