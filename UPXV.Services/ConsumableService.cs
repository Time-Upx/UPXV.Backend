using FluentValidation;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class ConsumableService : ServiceBase<Consumable>
{
   private ConsumableRepository _repository => (ConsumableRepository) _repositoryBase;
   public ConsumableService (RepositoryBase<Consumable> repository, IValidator<Consumable> validator) : base(repository, validator)
   {
   }
}