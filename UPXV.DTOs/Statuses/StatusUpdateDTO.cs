using UPXV.Models;

namespace UPXV.DTOs.Statuses;

public record StatusUpdateDTO
{
   public required int Nid { get; set; }
   public string? Tid { get; set; }
   public string? Description { get; set; }

   public void UpdateEntity (Status status)
   {
      if (Tid is not null) status.Tid = Tid;
      if (Description is not null) status.Description = Description;
   }
}
