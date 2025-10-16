using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Intents;

public record IntentListDTO
{
   public int Id { get; set; }
   public required string Name { get; set; }

   public static IntentListDTO Of (Intent intent) => new()
   {
      Id = intent.Id,
      Name = intent.Name,
   };
}
