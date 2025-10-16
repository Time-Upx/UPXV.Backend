using Microsoft.EntityFrameworkCore;

namespace UPXV.Backend.Entities;

public class Status : INamedEntity
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }

   public void LoadRequirements (DbContext context)
   {
      throw new NotImplementedException();
   }
}