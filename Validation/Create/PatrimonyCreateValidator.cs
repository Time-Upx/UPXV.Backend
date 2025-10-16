using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Create;

public class PatrimonyCreateValidator : AbstractValidator<PatrimonyCreateDTO>
{
   public PatrimonyCreateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .NotEmpty().WithMessage("O nome é obrigatório")
         .Must(name => !context.Exists<Patrimony>(name))
         .WithMessage("Nome já está sendo utilizado");

      RuleFor(dto => dto.StatusId)
         .GreaterThan(0).WithMessage("O id do Status deve ser maior que zero")
         .Must(id => context.Exists<Unit>(id))
         .WithMessage("Status não existe");

      RuleFor(dto => dto.TagIds)
         .Must(tags => tags.All(id => id > 0))
         .WithMessage("Todos os ids de tags devem ser maiores que zero")
         .Must(tags => tags.All(id => context.Exists<Tag>(id)))
         .WithMessage("Não foi possível localizar algumas Tags");
   }
}
