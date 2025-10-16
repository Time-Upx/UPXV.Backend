using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Patrimonies;

public record PatrimonyListDTO
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public IEnumerable<TagListDTO> Tags { get; set; } = [];
   public required StatusListDTO Status { get; set; }
   public static PatrimonyListDTO Of (Patrimony patrimony) => new PatrimonyListDTO
   {
      Id = patrimony.Id,
      Name = patrimony.Name,
      Tags = patrimony.Tags.Select(TagListDTO.Of),
      Status = StatusListDTO.Of(patrimony.Status!),
   };
}