using UPXV.Models;

namespace UPXV.DTOs.Statuses;

public record StatusDetailDTO
{
   public required int Nid { get; set; }
   public required string Tid { get; set; }
   public string? Description { get; set; }

   public static StatusDetailDTO Of (Status status) => new StatusDetailDTO
   {
      Nid = status.Nid,
      Tid = status.Tid,
      Description = status.Description,
   };
}
