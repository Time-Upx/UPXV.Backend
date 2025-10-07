namespace UPXV.Backend.API.Entities;

public class Tag
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }
}