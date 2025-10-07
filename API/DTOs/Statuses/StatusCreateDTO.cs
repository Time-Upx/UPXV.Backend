using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Statuses;

public record StatusCreateDTO
{
   public required string Name { get; set; }
   public string? Description { get; set; }

   public Status BuildEntity () => new Status
   {
      Name = Name,
      Description = Description,
   };
}
