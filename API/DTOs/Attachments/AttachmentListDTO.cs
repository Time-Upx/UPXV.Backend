using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Attachments;

public record AttachmentListDTO
{
   public int Id { get; set; }
   public string? Description { get; set; }
   public required string FullName { get; set; }

   public static AttachmentListDTO Of (Attachment attachment) => new AttachmentListDTO
   {
      Id = attachment.Id,
      Description = attachment.Description,
      FullName = attachment.FullName,
   };
}
