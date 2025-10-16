using Microsoft.EntityFrameworkCore;
using UPXV.Backend.Data;

namespace UPXV.Backend.Entities;

public class Patrimony : IHasRequirements, INamedEntity
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }
   public ICollection<Tag> Tags { get; set; } = [];
   public string? Identifier { get; set; }
   public DateTime RegisteredIn { get; set; } = DateTime.Now;
   public string? RegisteredBy { get; set; }
   public int StatusId { get; set; }
   public Status? Status { get; set; }

   public void LoadRequirements (DbContext context)
   {
      context.Load(this, p => p.Status);
      context.LoadCollection(this, p => p.Tags);
   }
}