using UPXV.Models;

namespace UPXV.DTOs.Tags;

public class TagDetailDTO
{
   public required int Nid { get; set; }
   public required string Tid { get; set; }
   public string? Description { get; set; }

   public static TagDetailDTO Of (Tag tag) => new TagDetailDTO
   {
      Nid = tag.Nid,
      Tid = tag.Tid,
      Description = tag.Description,
   };
}
