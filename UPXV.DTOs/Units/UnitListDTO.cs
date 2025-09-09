using UPXV.Models;

namespace UPXV.Dto.UnitDTOs;

public record class UnitListDTO(int UnitNid, string UnitTid)
{
   public UnitListDTO (Unit unit) : this(unit.Nid, unit.Tid) { }
}
