using Microsoft.EntityFrameworkCore;
using UPXV.Backend.Data;

namespace UPXV.Backend.Entities;

public class Consumable : IHasRequirements, INamedEntity
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }
   public ICollection<Tag> Tags { get; set; } = [];
   public double Quantity { get; set; }
   public int UnitId { get; set; }
   public Unit? Unit { get; set; }

   public void SetTags (ICollection<Tag> tags)
   {
      var tagsToRemove = Tags
            .Where(existingTag => !tags.Any(newTag => newTag.Id == existingTag.Id))
            .ToList();

      var tagsToAdd = tags
          .Where(newTag => !Tags.Any(existingTag => existingTag.Id == newTag.Id))
          .ToList();

      foreach (var tag in tagsToRemove) Tags.Remove(tag);
      foreach (var tag in tagsToAdd) Tags.Add(tag);
   }
   public void LoadRequirements (DbContext context)
   {
      context.Load(this, c => c.Unit);
      context.LoadCollection(this, c => c.Tags);
   }
}
