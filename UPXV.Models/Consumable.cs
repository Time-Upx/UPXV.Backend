namespace UPXV.Models;

public class Consumable : Item, IBaseModel
{
   public double Quantity { get; set; }
   public int UnitId { get; set; }
   public Unit? Unit { get; set; }
}
