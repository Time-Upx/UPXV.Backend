namespace UPXV.Models;

public class Tag : IEntityBase
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public string Description { get; set; } = string.Empty;
}
