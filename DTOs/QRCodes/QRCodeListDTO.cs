using UPXV.Backend.DTOs.Intents;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.QRCodes;

public record QRCodeListDTO
{
   public required string Id { get; set; }
   public IntentListDTO? Intent { get; set; }
   public string? Name { get; set; }
   public bool HasExpired { get; set; }
   public bool HasReachedUsageLimit { get; set; }

   public static QRCodeListDTO Of (QRCode qrcode) => new()
   {
      Id = qrcode.Id,
      Intent = IntentListDTO.Of (qrcode.Intent!),
      Name = qrcode.Name,
      HasExpired = qrcode.HasExpired,
      HasReachedUsageLimit = qrcode.HasReachedUsageLimit,
   };
}
