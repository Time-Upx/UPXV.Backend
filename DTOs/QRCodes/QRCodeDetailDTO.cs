using FluentValidation.Results;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.DTOs.Intents;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.QRCodes;

public record QRCodeDetailDTO
{
   public int Id { get; set; }
   public string? Url { get; set; }
   public IntentDetailDTO? Intent { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public DateTime? Expiration { get; set; }
   public string? Password { get; set; }
   public int? UsageLimit { get; set; }
   public int TimesUsed { get; set; }
   public IDictionary<string, string> Arguments { get; set; } = new Dictionary<string, string>();

   public static QRCodeDetailDTO Of(QRCode qrcode, string url)
   {
      return new QRCodeDetailDTO
      {
         Id = qrcode.Id,
         Url = url,
         Intent = IntentDetailDTO.Of(qrcode.Intent!),
         Name = qrcode.Name,
         Description = qrcode.Description,
         Expiration = qrcode.Expiration,
         Password = qrcode.Password,
         UsageLimit = qrcode.UsageLimit,
         TimesUsed = qrcode.TimesUsed,
         Arguments = qrcode.Arguments.ToDictionary(),
      };
   }
   public static bool TryCreate(QRCode qrcode, ApplicationConfiguration appConfig, out QRCodeDetailDTO dto, out ValidationResult outResult)
   {
      if (!qrcode.TryGetUrl(appConfig, out string url, out ValidationResult result))
      {
         dto = null!;
         outResult = result;
         return false;
      }

      dto = new()
      {
         Id = qrcode.Id,
         Url = url,
         Intent = IntentDetailDTO.Of(qrcode.Intent!),
         Name = qrcode.Name,
         Description = qrcode.Description,
         Expiration = qrcode.Expiration,
         Password = qrcode.Password,
         UsageLimit = qrcode.UsageLimit,
         TimesUsed = qrcode.TimesUsed,
         Arguments = qrcode.Arguments.ToDictionary(),
      };
      outResult = null!;
      return true;
   }
}
