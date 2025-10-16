using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Units;

public record UnitDetailDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public required string Abbreviation { get; set; }
   public static UnitDetailDTO Of (Unit unit) => new UnitDetailDTO
   {
      Id = unit.Id,
      Name = unit.Name,
      Abbreviation = unit.Abbreviation,
   };
}
