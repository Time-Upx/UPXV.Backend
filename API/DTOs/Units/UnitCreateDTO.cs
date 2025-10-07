using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Units;

public record UnitCreateDTO
{
   public required string Name { get; set; }
   public required string Abbreviation { get; set; }
   public Unit BuildEntity () => new Unit
   {
      Name = Name,
      Abbreviation = Abbreviation,
   };
}
