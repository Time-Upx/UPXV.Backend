using FluentValidation;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class ConsumableService (ConsumableRepository repository, IValidator<Consumable> validator) 
   : ServiceBase<Consumable>(repository, validator)
{
   protected new ConsumableRepository _repository => (ConsumableRepository) _repository;
}