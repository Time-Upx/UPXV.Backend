namespace UPXV.Models;

public class Consumable : IEntityBase
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public int ItemNid { get; set; }
   public Item? Item { get; set; }
   public double Quantity { get; set; }
   public int UnitNid { get; set; }
   public Unit? Unit { get; set; }
}
