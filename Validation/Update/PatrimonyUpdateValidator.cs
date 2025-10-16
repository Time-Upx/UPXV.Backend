using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Update;

public class PatrimonyUpdateValidator : AbstractValidator<PatrimonyUpdateDTO>
{
   public PatrimonyUpdateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.Name)
         .Must(name => !context.ExistsOtherWithName<Patrimony>(name!))
         .WithMessage("Nome já está sendo utilizado")
         .Unless(dto => dto.Name is null);

      RuleFor(dto => dto.StatusId)
         .GreaterThan(0).WithMessage("O id do Status deve ser maior que zero")
         .Must(id => context.Exists<Status>(id))
         .WithMessage("Status não existe")
         .Unless(dto => dto.StatusId is null);

      RuleFor(dto => dto.TagIds)
         .Must(tags => tags!.All(id => id > 0))
         .WithMessage("Todos os ids de tags devem ser maiores que zero")
         .Must(tags => tags!.All(id => context.Exists<Tag>(id)))
         .WithMessage("Não foi possível localizar algumas Tags")
         .Unless(dto => dto.Name is null);
   }
}