using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Tags;

public class TagDetailDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }

   public static TagDetailDTO Of (Tag tag) => new TagDetailDTO
   {
      Id = tag.Id,
      Name = tag.Name,
      Description = tag.Description,
   };
}
