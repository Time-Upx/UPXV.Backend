using UPXV.Backend.API.DTOs.Statuses;
using UPXV.Backend.API.DTOs.Tags;
using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Patrimonies;

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