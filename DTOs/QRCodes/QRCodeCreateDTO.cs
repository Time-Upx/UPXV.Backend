using FluentValidation.Results;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.QRCodes;

public record QRCodeCreateDTO
{
   public int IntentId { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public DateTime? Expiration { get; set; }
   public string? Password { get; set; }
   public int? UsageLimit { get; set; }
   public Dictionary<string, string> IntentArguments { get; set; } = [];

   public bool TryBuildEntity (Intent intent, IDictionary<string, string> arguments, ApplicationConfiguration appConfig, out QRCode qrcode, out string outUrl, out ValidationResult outResult) 
   {
      if (!QRCodeExtensions.TryBuildUrl(intent, appConfig, arguments, out string url, out ValidationResult result))
      {
         qrcode = null!;
         outResult = result;
         outUrl = null!;
         return false;
      }

      qrcode = new()
      {
         IntentId = IntentId,
         Intent = intent,
         Name = Name,
         Description = Description,
         Expiration = Expiration,
         Password = Password,
         UsageLimit = UsageLimit,
      };

      outResult = null!;
      outUrl = url;
      return true;
   }
}