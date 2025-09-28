using UPXV.Models;

namespace UPXV.DTOs.Tags;

public record TagCreateDTO
{
   public required string Tid { get; set; }
   public string? Description { get; set; }

   public Tag BuildEntity () => new Tag
   {
      Tid = Tid,
      Description = Description
   };
}
