using UPXV.Models;

namespace UPXV.DTOs.Consumables;

public record ConsumableUpdateDTO
{
   public required int Nid { get; set; }
   public string? Tid { get; set; }
   public string? Description { get; set; }
   public IEnumerable<int>? TagNids { get; set; }
   public double? Quantity { get; set; }
   public int? UnitNid { get; set; }
   public void UpdateEntity (Consumable consumable, ICollection<Tag>? tags)
   {
      if (Tid is not null) consumable.Tid = Tid;
      if (Description is not null) consumable.Description = Description;
      if (Quantity is not null) consumable.Quantity = Quantity.Value;
      if (UnitNid is not null) consumable.UnitNid = UnitNid.Value;
      if (tags is not null) consumable.Tags = tags;
   }
}
