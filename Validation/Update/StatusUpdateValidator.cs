using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Update;

public class StatusUpdateValidator : AbstractValidator<StatusUpdateDTO>
{
   public StatusUpdateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .Must(name => !context.ExistsOtherWithName<Status>(name!))
         .WithMessage("Nome já está sendo utilizado")
         .Unless(dto => dto.Name is null);
   }
}
