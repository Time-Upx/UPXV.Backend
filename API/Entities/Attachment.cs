namespace UPXV.Backend.API.Entities;

public class Attachment
{
   public int Id { get; set; }
   public string? Description { get; set; }
   public required string FileName { get; set; }
   public required string FileExtension { get; set; }
   public required string FileUID { get; set; }
   public string? DirectoryLocation { get; set; }

   public string FullName => FileName + FileExtension;
   public string FullUID => FileUID + FileExtension;
   public string Path => DirectoryLocation + FullName;
}
