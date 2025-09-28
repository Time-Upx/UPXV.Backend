using UPXV.Models;

namespace UPXV.DTOs.Tags;

public record TagUpdateDTO
{
   public required int Nid { get; set; }
   public string? Tid { get; set; }
   public string? Description { get; set; }

   public void UpdateEntity (Tag entity)
   {
      if (Tid is not null) entity.Tid = Tid;
      if (Description is not null) entity.Description = Description;
   }
}
