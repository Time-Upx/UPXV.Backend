namespace UPXV.Models;

public class Unit : IEntityBase
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public required string Abbreviation { get; set; }
}
