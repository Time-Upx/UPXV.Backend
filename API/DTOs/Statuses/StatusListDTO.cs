using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Statuses;
public record StatusListDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }

   public static StatusListDTO Of (Status status) => new StatusListDTO
   {
      Id = status.Id,
      Name = status.Name,
      Description = status.Description,
   };
}