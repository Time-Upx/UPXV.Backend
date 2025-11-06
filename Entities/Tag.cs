
namespace UPXV.Backend.Entities;

public class Tag : INamedEntity
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }

   public override bool Equals (object? obj) => obj is not null && obj is Tag t && Id == t.Id;
   public override int GetHashCode () => HashCode.Combine(Id);
}