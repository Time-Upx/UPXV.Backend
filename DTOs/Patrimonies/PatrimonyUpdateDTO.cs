using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Patrimonies;

public record PatrimonyUpdateDTO
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public int[]? TagIds { get; set; }
   public int? StatusId { get; set; }

   public void UpdateEntity (Patrimony patrimony, ICollection<Tag>? tags)
   {
      if (Name is not null) patrimony.Name = Name;
      if (Description is not null) patrimony.Description = Description;
      if (StatusId is not null) patrimony.StatusId = StatusId.Value;
      if (tags is not null) patrimony.SetTags(tags);
   }
}
