using UPXV.Models;

namespace UPXV.DTOs.Statuses;

public record StatusCreateDTO
{
   public required string Tid { get; set; }
   public string? Description { get; set; }

   public Status BuildEntity () => new Status
   {
      Tid = Tid,
      Description = Description,
   };
}
