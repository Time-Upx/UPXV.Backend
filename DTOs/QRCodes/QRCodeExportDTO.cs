namespace UPXV.Backend.DTOs.QRCodes;

public record QRCodeExportDTO
{
   public int? Width { get; set; }
   public int? Height { get; set; }
   public int? Margin { get; set; }
   public int? Quality { get; set; }
}
