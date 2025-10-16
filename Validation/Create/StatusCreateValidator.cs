using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Create;

public class StatusCreateValidator : AbstractValidator<StatusCreateDTO>
{
   public StatusCreateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .NotEmpty().WithMessage("O nome é obrigatório")
         .Must(name => !context.Exists<Status>(name!))
         .WithMessage("Nome já está sendo utilizado")
         .Unless(dto => dto.Name is null);
   }
}