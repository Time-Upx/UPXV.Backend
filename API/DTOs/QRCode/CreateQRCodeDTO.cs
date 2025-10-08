namespace UPXV.Backend.API.DTOs.QRCode;

public record CreateQRCodeDTO
{
   public int Width { get; set; }
   public int Height { get; set; }
   public int Margin { get; set; }
}