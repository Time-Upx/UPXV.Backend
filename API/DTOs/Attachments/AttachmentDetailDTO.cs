using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Attachments;

public record AttachmentDetailDTO
{
   public int Id { get; set; }
   public string? Description { get; set; }
   public required string FileName { get; set; }
   public required string FileExtension { get; set; }
   public required string FileUID { get; set; }

   public string FullName => FileName + FileExtension;
   public string FullUID => FileUID + FileExtension;

   public static AttachmentDetailDTO Of (Attachment attachment) => new AttachmentDetailDTO
   {
      Id = attachment.Id,
      Description = attachment.Description,
      FileName = attachment.FileName,
      FileUID = attachment.FileUID,
      FileExtension = attachment.FileExtension,
   };
}
