using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Patrimonies;

public record PatrimonyCreateDTO
{
   public required string Name { get; set; }
   public string? Description { get; set; }
   public IEnumerable<int> TagIds { get; set; } = [];
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
