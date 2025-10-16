namespace UPXV.Backend.Entities;

public class Tag : INamedEntity
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }
}