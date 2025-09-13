using FluentValidation;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class UnitService(UnitRepository repository, IValidator<Unit> validator) 
   : ServiceBase<Unit> (repository, validator)
{
   protected new UnitRepository _repository => (UnitRepository) _repository;
}
