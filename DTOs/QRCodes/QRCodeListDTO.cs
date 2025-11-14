using UPXV.Backend.DTOs.Intents;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.QRCodes;

public record QRCodeListDTO
{
   public required string Id { get; set; }
   public IntentListDTO? Intent { get; set; }
   public string? Name { get; set; }
   public DateTime CreatedAt { get; set; }
   public DateTime? Expiration { get; set; }
   public int? UsageLimit { get; set; }
   public int TimesUsed { get; set; }


   public static QRCodeListDTO Of(QRCode qrcode) => new()
   {
      Id = qrcode.Id,
      Intent = IntentListDTO.Of(qrcode.Intent!),
      Name = qrcode.Name,
      CreatedAt = qrcode.CreatedAt,
      Expiration = qrcode.Expiration,
      UsageLimit = qrcode.UsageLimit,
      TimesUsed = qrcode.TimesUsed,
   };
}
