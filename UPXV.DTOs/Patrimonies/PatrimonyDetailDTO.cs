using UPXV.DTOs.Statuses;
using UPXV.DTOs.Tags;
using UPXV.Models;

namespace UPXV.DTOs.Patrimonies;

public record PatrimonyDetailDTO
{
   public required int Nid { get; set; }
   public required string Tid { get; set; }
   public string? Description { get; set; }
   public IEnumerable<TagDetailDTO> Tags { get; set; } = [];
   public required DateTime RegisteredIn { get; set; }
   public string? RegisteredBy { get; set; }
   public required StatusDetailDTO Status { get; set; }

   public static PatrimonyDetailDTO Of (Patrimony patrimony) => new PatrimonyDetailDTO
   {
      Nid = patrimony.Nid,
      Tid = patrimony.Tid,
      Description = patrimony.Description,
      Tags = patrimony.Tags.Select(TagDetailDTO.Of),
      RegisteredIn = patrimony.RegisteredIn,
      RegisteredBy = patrimony.RegisteredBy,
      Status = StatusDetailDTO.Of(patrimony.Status!)
   };
}
