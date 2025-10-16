using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Update;

public class ConsumableUpdateValidator : AbstractValidator<ConsumableUpdateDTO>
{
   public ConsumableUpdateValidator(UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .Must(name => !context.ExistsOtherWithName<Consumable>(name!))
         .WithMessage("Nome já está sendo utilizado")
         .Unless(dto => dto.Name is null);

      RuleFor(dto => dto.UnitId)
         .GreaterThan(0).WithMessage("O id da unidade deve ser maior que zero")
         .Must(id => context.Exists<Unit>(id))
         .WithMessage("Unidade não existe")
         .Unless(dto => dto.UnitId is null);

      RuleFor(dto => dto.Quantity)
         .GreaterThanOrEqualTo(0).WithMessage("A quantidade deve ser maior ou igual a zero")
         .Unless(dto => dto.Quantity is null);

      RuleFor(dto => dto.TagIds)
         .Must(tags => tags!.All(id => id > 0))
         .WithMessage("Todos os ids de tags devem ser maiores que zero")
         .Must(tags => tags!.All(id => context.Exists<Tag>(id)))
         .WithMessage("Não foi possível localizar algumas Tags")
         .Unless(dto => dto.TagIds is null);
   }
}
