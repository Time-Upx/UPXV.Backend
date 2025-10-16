using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Patrimonies;

public record PatrimonyCreateDTO
{
   public required string Name { get; set; }
   public string? Description { get; set; }
   public int[] TagIds { get; set; } = [];
   public string? RegisteredBy { get; set; }
   public int StatusId { get; set; }

   public Patrimony BuildEntity (ICollection<Tag> tags) => new Patrimony
   {
      Name = Name,
      Description = Description,
      RegisteredBy = RegisteredBy,
      StatusId = StatusId,
      Tags = tags
   };
}
