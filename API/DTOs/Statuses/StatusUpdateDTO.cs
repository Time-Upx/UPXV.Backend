using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Statuses;

public record StatusUpdateDTO
{
   public required int Id { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }

   public void UpdateEntity (Status status)
   {
      if (Name is not null) status.Name = Name;
      if (Description is not null) status.Description = Description;
   }
}
