namespace UPXV.Backend.Entities;

public class Intent : INamedEntity
{
   public int Id { get; set; }
   public required IntentType Type { get; set; }
   public required string Name { get; set; }
   public required string Description { get; set; }
   public required string UrlTemplate { get; set; }
   public required string[] Parameters { get; set; }
}
