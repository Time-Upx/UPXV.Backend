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

   public void SetTags(ICollection<Tag> tags)
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
      context.Load(this, p => p.Status);
      context.LoadCollection(this, p => p.Tags);
   }
}