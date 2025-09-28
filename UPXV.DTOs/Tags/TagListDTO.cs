using UPXV.Models;

namespace UPXV.DTOs.Tags;

public record TagListDTO
{
   public required int Nid { get; set; }
   public required string Tid { get; set; }

   public static TagListDTO Of (Tag tag) => new TagListDTO
   {
      Nid = tag.Nid,
      Tid = tag.Tid,
   };
}