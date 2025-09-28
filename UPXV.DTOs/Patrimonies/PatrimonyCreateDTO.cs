using UPXV.Models;

namespace UPXV.DTOs.Patrimonies;

public record PatrimonyCreateDTO
{
   public required string Tid { get; set; }
   public string? Description { get; set; }
   public IEnumerable<int> TagNids { get; set; } = [];
   public string? RegisteredBy { get; set; }
   public int StatusNid { get; set; }

   public Patrimony BuildEntity (ICollection<Tag> tags) => new Patrimony
   {
      Tid = Tid,
      Description = Description,
      RegisteredBy = RegisteredBy,
      StatusNid = StatusNid,
      Tags = tags
   };
}
