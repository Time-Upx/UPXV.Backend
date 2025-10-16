using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Patrimonies;

public record PatrimonyDetailDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }
   public IEnumerable<TagDetailDTO> Tags { get; set; } = [];
   public required DateTime RegisteredIn { get; set; }
   public string? RegisteredBy { get; set; }
   public required StatusDetailDTO Status { get; set; }

   public static PatrimonyDetailDTO Of (Patrimony patrimony) => new PatrimonyDetailDTO
   {
      Id = patrimony.Id,
      Name = patrimony.Name,
      Description = patrimony.Description,
      Tags = patrimony.Tags.Select(TagDetailDTO.Of),
      RegisteredIn = patrimony.RegisteredIn,
      RegisteredBy = patrimony.RegisteredBy,
      Status = StatusDetailDTO.Of(patrimony.Status!)
   };
}
