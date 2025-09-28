using UPXV.Models;

namespace UPXV.DTOs.Units;

public record UnitCreateDTO
{
   public required string Tid { get; set; }
   public required string Abbreviation { get; set; }
   public Unit BuildEntity () => new Unit
   {
      Tid = Tid,
      Abbreviation = Abbreviation,
   };
}
