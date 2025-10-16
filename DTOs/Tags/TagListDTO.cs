using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Tags;

public class TagListDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }

   public static TagListDTO Of (Tag tag) => new TagListDTO
   {
      Id = tag.Id,
      Name = tag.Name,
      Description = tag.Description,
   };
}
