using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Units;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Create;

public class UnitCreateValidator : AbstractValidator<UnitCreateDTO>
{
   public UnitCreateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .NotEmpty().WithMessage("O nome é obrigatório")
         .Must(name => !context.Exists<Unit>(name!))
         .WithMessage("Nome já está sendo utilizado")
         .Unless(dto => dto.Name is null);
   }
}