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
   public Dictionary<string, string> IntentParameters { get; set; } = [];

   public QRCode BuildEntity () => new()
   {
      Id = Guid.NewGuid().ToString(),
      IntentId = IntentId,
      Name = Name,
      Description = Description,
      Expiration = Expiration,
      Password = Password,
      UsageLimit = UsageLimit,
      IntentParameters = IntentParameters,
   };
}