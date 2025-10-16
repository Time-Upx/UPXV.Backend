using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Create;

public class TagCreateValidator : AbstractValidator<TagCreateDTO>
{
   public TagCreateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .NotEmpty().WithMessage("O nome é obrigatório")
         .Must(name => !context.Exists<Tag>(name!))
         .WithMessage("Nome já está sendo utilizado")
         .Unless(dto => dto.Name is null);
   }
}