using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Units;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Update;

public class UnitUpdateValidator : AbstractValidator<UnitUpdateDTO>
{
   public UnitUpdateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .Must(name => !context.ExistsOtherWithName<Unit>(name!))
         .WithMessage("Nome já está sendo utilizado")
         .Unless(dto => dto.Name is null);
   }
}