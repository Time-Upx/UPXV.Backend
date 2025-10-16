namespace UPXV.Backend.Entities;

public class Unit : INamedEntity
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required string Abbreviation { get; set; }
}
