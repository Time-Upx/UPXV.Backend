using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Consumables;

public record ConsumableUpdateDTO
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public int[]? TagIds { get; set; }
   public double? Quantity { get; set; }
   public int? UnitId { get; set; }
   public void UpdateEntity (Consumable consumable, ICollection<Tag>? tags)
   {
      if (Name is not null) consumable.Name = Name;
      if (Description is not null) consumable.Description = Description;
      if (Quantity is not null) consumable.Quantity = Quantity.Value;
      if (UnitId is not null) consumable.UnitId = UnitId.Value;
      if (tags is not null) consumable.SetTags(tags);
   }
}