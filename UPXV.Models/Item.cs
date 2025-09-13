namespace UPXV.Models;

public class Item : IEntityBase
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public string Description { get; set; } = string.Empty;
   public ICollection<Tag> Tags { get; set; } = [];
}
