using UPXV.DTOs.Statuses;
using UPXV.DTOs.Tags;
using UPXV.Models;

namespace UPXV.DTOs.Patrimonies;

public record PatrimonyListDTO
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public IEnumerable<TagListDTO> Tags { get; set; } = [];
   public required StatusListDTO Status { get; set; }
   public static PatrimonyListDTO Of (Patrimony patrimony) => new PatrimonyListDTO
   {
      Nid = patrimony.Nid,
      Tid = patrimony.Tid,
      Tags = patrimony.Tags.Select(TagListDTO.Of),
      Status = StatusListDTO.Of(patrimony.Status!),
   };
}