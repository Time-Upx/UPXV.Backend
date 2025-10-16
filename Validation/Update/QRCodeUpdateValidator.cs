using FluentValidation;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Validation.Update;

public class QRCodeUpdateValidator : AbstractValidator<QRCodeUpdateDTO>
{
   public QRCodeUpdateValidator (UPXV_Context context)
   { 
      RuleFor(dto => dto.IntentId)
         .GreaterThan(0).WithMessage("O id da unidade deve ser maior que zero")
         .Must(id => context.Exists<Unit>(id))
         .WithMessage("Unidade não existe")
         .Unless(dto => dto.IntentId is null);

      RuleFor(dto => dto.UsageLimit)
         .Must(usageLimit => usageLimit > 0)
         .WithMessage("Quantidade de usos não pode ser negativa")
         .Unless(dto => dto.UsageLimit is null);
   }
}