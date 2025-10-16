using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Statuses;

public record StatusDetailDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }

   public static StatusDetailDTO Of (Status status) => new StatusDetailDTO
   {
      Id = status.Id,
      Name = status.Name,
      Description = status.Description,
   };
}
