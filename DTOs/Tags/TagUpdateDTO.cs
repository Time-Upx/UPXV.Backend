using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Tags;

public record TagUpdateDTO
{
   public string? Name { get; set; }
   public string? Description { get; set; }

   public void UpdateEntity (Tag entity)
   {
      if (Name is not null) entity.Name = Name;
      if (Description is not null) entity.Description = Description;
   }
}
