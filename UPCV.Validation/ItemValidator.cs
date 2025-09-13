using FluentValidation;
using UPXV.Models;

namespace UPCV.Validation;

public class ItemValidator : AbstractValidator<Item>
{
   public ItemValidator()
   {
      RuleFor(x => x.Tid).NotEmpty();
   }
}
