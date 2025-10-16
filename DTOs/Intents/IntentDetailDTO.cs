using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Intents;

public record IntentDetailDTO
{
   public int Id { get; set; }
   public required IntentType Type { get; set; }
   public required string Name { get; set; }
   public required string Description { get; set; }

   public static IntentDetailDTO Of (Intent intent) => new()
   {
      Id = intent.Id,
      Type = intent.Type,
      Name = intent.Name,
      Description = intent.Description,
   };
}
