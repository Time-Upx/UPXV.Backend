using UPXV.Models;

namespace UPXV.DTOs.Statuses;
public record StatusListDTO
{
   public required int Nid { get; set; }
   public required string Tid { get; set; }

   public static StatusListDTO Of (Status status) => new StatusListDTO
   {
      Nid = status.Nid,
      Tid = status.Tid,
   };
}