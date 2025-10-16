using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Statuses;

public record StatusUpdateDTO
{
   public string? Name { get; set; }
   public string? Description { get; set; }

   public void UpdateEntity (Status status)
   {
      if (Name is not null) status.Name = Name;
      if (Description is not null) status.Description = Description;
   }
}
