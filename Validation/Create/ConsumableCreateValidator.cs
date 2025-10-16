using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Create;

public class ConsumableCreateValidator : AbstractValidator<ConsumableCreateDTO>
{
   public ConsumableCreateValidator(UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .NotEmpty().WithMessage("O nome é obrigatório")
         .Must(name => !context.Exists<Consumable>(name))
         .WithMessage("Nome já está sendo utilizado");

      RuleFor(dto => dto.UnitId)
         .GreaterThan(0).WithMessage("O id da unidade deve ser maior que zero")
         .Must(id => context.Exists<Unit>(id))
         .WithMessage("Unidade não existe");

      RuleFor(dto => dto.Quantity)
         .GreaterThanOrEqualTo(0).WithMessage("A quantidade deve ser maior ou igual a zero");

      RuleFor(dto => dto.TagIds)
         .Must(tags => tags.All(id => id > 0))
         .WithMessage("Todos os ids de tags devem ser maiores que zero")
         .Must(tags => tags.All(id => context.Exists<Tag>(id)))
         .WithMessage("Não foi possível localizar algumas Tags");
   }
}
