using FluentValidation;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class UnitService : ServiceBase<Unit>
{
   private UnitRepository _repository => (UnitRepository) _repositoryBase;
   public UnitService (UnitRepository repository, IValidator<Unit> validator) : base(repository, validator) 
   {
   }




}
