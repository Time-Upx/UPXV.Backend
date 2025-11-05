namespace UPXV.Backend.Entities;

public class QRCodeArgument
{
   public int Id { get; set; }
   public string QRCodeId { get; set; }
   public QRCode? QRCode { get; set; }
   public string Parameter { get; set; }
   public string Value { get; set; }

   public QRCodeArgument() { }
   public QRCodeArgument(int id, string qrCodeId, string parameter, object value)
   {
      Id = id;
      QRCodeId = qrCodeId;
      Parameter = parameter;
      Value = value.ToString() ?? "null";
   }
}
