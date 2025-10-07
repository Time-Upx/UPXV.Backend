using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Tags;

public record TagUpdateDTO
{
   public required int Id { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }

   public void UpdateEntity (Tag entity)
   {
      if (Name is not null) entity.Name = Name;
      if (Description is not null) entity.Description = Description;
   }
}
