namespace UPXV.Backend.Entities;

public class QRCodeArgument
{
   public int Id { get; set; }
   public int QRCodeId { get; set; }
   public QRCode? QRCode { get; set; }
   public required string Parameter { get; set; }
   public required string Value { get; set; }
}
