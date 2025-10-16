using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Update;

public class TagUpdateValidator : AbstractValidator<TagUpdateDTO>
{
   public TagUpdateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .Must(name => !context.ExistsOtherWithName<Tag>(name!))
         .WithMessage("Nome já está sendo utilizado")
         .Unless(dto => dto.Name is null);
   }
}