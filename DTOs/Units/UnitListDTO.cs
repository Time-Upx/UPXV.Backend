using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Units;

public record UnitListDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public required string Abbreviation { get; set; }

   public static UnitListDTO Of (Unit unit) => new UnitListDTO
   {
      Id = unit.Id,
      Name = unit.Name,
      Abbreviation = unit.Abbreviation,
   };
}
