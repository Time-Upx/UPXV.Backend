using UPXV.Backend.DTOs.Intents;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.QRCodes;

public record QRCodeDetailDTO
{
   public required string Id { get; set; }
   public string? Url { get; set; }
   public IntentDetailDTO? Intent { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public DateTime? Expiration { get; set; }
   public string? Password { get; set; }
   public int? UsageLimit { get; set; }
   public int TimesUsed { get; set; }

   public static QRCodeDetailDTO Of (QRCode qrcode) => new()
   {
      Id = qrcode.Id.ToString(),
      Intent = IntentDetailDTO.Of(qrcode.Intent!),
      Name = qrcode.Name,
      Description = qrcode.Description,
      Expiration = qrcode.Expiration,
      Password = qrcode.Password,
      UsageLimit = qrcode.UsageLimit,
      TimesUsed = qrcode.TimesUsed,
   };
}
