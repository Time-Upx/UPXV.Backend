using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.QRCodes;

public record QRCodeUpdateDTO
{
   public int? IntentId { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Password { get; set; }
   public int? UsageLimit { get; set; }

   public void UpdateEntity(QRCode qrcode)
   {
      if (IntentId is not null) qrcode.IntentId = IntentId!.Value;
      if (Name is not null) qrcode.Name = Name;
      if (Description is not null) qrcode.Description = Description;
      if (Password is not null) qrcode.Password = Password;
      if (UsageLimit is not null) qrcode.UsageLimit = UsageLimit!.Value;
   }
}
