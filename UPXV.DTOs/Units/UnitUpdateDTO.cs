using UPXV.Models;

namespace UPXV.DTOs.Units;

public record UnitUpdateDTO
{
   public int Nid { get; set; }
   public string? Tid { get; set; }
   public string? Abbreviation { get; set; }

   public void UpdateEntity (Unit unit)
   {
      if (Tid is not null) unit.Tid = Tid;
      if (Abbreviation is not null) unit.Abbreviation = Abbreviation;
   }
}
