using UPXV.Models;

namespace UPXV.DTOs.Units;

public record UnitDetailDTO
{
   public required int Nid { get; set; }
   public required string Tid { get; set; }
   public required string Abbreviation { get; set; }
   public static UnitDetailDTO Of (Unit unit) => new UnitDetailDTO
   {
      Nid = unit.Nid,
      Tid = unit.Tid,
      Abbreviation = unit.Abbreviation,
   };
}
