using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Intents;

public record IntentDetailDTO
{
   public int Id { get; set; }
   public required string Type { get; set; }
   public required string Name { get; set; }
   public required string Description { get; set; }
   public string[] Parameters { get; set; } = [];

   public static IntentDetailDTO Of (Intent intent) => new()
   {
      Id = intent.Id,
      Type = Enum.GetName(intent.Type) ?? Enum.GetName(IntentType.None)!,
      Name = intent.Name,
      Description = intent.Description,
      Parameters = intent.Parameters.Select(ip => ip.Parameter).ToArray(),
   };
}
