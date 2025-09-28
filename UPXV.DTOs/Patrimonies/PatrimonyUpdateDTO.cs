using UPXV.Models;

namespace UPXV.DTOs.Patrimonies;

public record PatrimonyUpdateDTO
{
   public required int Nid { get; set; }
   public string? Tid { get; set; }
   public string? Description { get; set; }
   public IEnumerable<int>? TagNids { get; set; }
   public int? StatusNid { get; set; }

   public void UpdateEntity (Patrimony patrimony, ICollection<Tag>? tags)
   {
      if (Tid is not null) patrimony.Tid = Tid;
      if (Description is not null) patrimony.Description = Description;
      if (StatusNid is not null) patrimony.StatusNid = StatusNid.Value;
      if (tags is not null) patrimony.Tags = tags;
   }
}
