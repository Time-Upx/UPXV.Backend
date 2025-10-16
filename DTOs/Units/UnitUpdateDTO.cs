using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Units;

public record UnitUpdateDTO
{
   public string? Name { get; set; }
   public string? Abbreviation { get; set; }

   public void UpdateEntity (Unit unit)
   {
      if (Name is not null) unit.Name = Name;
      if (Abbreviation is not null) unit.Abbreviation = Abbreviation;
   }
}
