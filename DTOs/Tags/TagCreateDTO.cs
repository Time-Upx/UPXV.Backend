using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Tags;

public record TagCreateDTO
{
   public required string Name { get; set; }
   public string? Description { get; set; }

   public Tag BuildEntity () => new Tag
   {
      Name = Name,
      Description = Description
   };
}
