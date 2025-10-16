using FluentValidation;
using UPXV.Backend.DTOs.QRCodes;

namespace UPXV.Backend.Validation;

public class QRCodeExportValidator : AbstractValidator<QRCodeExportDTO>
{
   public QRCodeExportValidator ()
   {
      RuleFor(x => x.Width)
         .GreaterThan(0)
         .When(x => x.Width is not null)
         .WithMessage("Width must be greater than 0");
      RuleFor(x => x.Height)
         .GreaterThan(0)
         .When(x => x.Height is not null)
         .WithMessage("Height must be greater than 0");
      RuleFor(x => x.Margin)
         .GreaterThanOrEqualTo(0)
         .When(x => x.Margin is not null)
         .WithMessage("Margin must be positive");
      RuleFor(x => x.Quality)
         .GreaterThan(0)
         .When(x => x.Quality is not null)
         .WithMessage("Quality must be greater than 0");
   }
}
