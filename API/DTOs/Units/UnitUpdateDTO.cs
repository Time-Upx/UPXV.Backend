using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Units;

public record UnitUpdateDTO
{
   public int Id { get; set; }
   public string? Name { get; set; }
   public string? Abbreviation { get; set; }

   public void UpdateEntity (Unit unit)
   {
      if (Name is not null) unit.Name = Name;
      if (Abbreviation is not null) unit.Abbreviation = Abbreviation;
   }
}
