using Microsoft.EntityFrameworkCore;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Entities;

public class Status
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }

   public void LoadRequirements (DbContext context)
   {
      throw new NotImplementedException();
   }
}