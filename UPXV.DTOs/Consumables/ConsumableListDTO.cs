using UPXV.Models;

namespace UPXV.Dto.ConsumableDTOs;

public record ConsumableListDTO
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public double Quantity { get; set; }
   public int ItemId { get; set; }
   public Item? Item { get; set; }
   public int UnitId { get; set; }
   public Unit? Unit { get; set; }
}
