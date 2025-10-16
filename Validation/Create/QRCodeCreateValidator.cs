using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Create;

public class QRCodeCreateValidator : AbstractValidator<QRCodeCreateDTO>
{
   public QRCodeCreateValidator (UPXV_Context context)
   {
      RuleFor(dto => dto.IntentId)
         .GreaterThan(0).WithMessage("O id da Intenção deve ser maior que zero")
         .Must(id => context.Exists<Intent>(id))
         .WithMessage("Intenção não existe");

      RuleFor(dto => dto.Expiration)
         .GreaterThan(DateTime.Now)
         .WithMessage("Não é possível uma nova data de expiração estar no passado")
         .Unless(dto => dto.Expiration is null);

      RuleFor(dto => dto.UsageLimit)
         .Must(usageLimit => usageLimit > 0)
         .WithMessage("Quantidade de usos não pode ser negativa")
         .Unless(dto => dto.UsageLimit is null);
   }
}