using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Tags;

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
