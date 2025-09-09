namespace UPXV.Models;

public class Unit : IBaseModel
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public required string Abbreviation { get; set; }
}
