namespace UPXV.Backend.API.DTOs.QRCode;

public record CreateQRCodeDTO
{
   public virtual Type? Type { get; set; }
   public required string Data { get; set; }
   public int Width { get; set; } = 500;
   public int Height { get; set; } = 500;
   public int Margin { get; set; } = 10;
}

public record CreateQRCodeDTO<TContext> : CreateQRCodeDTO
{
   public override Type Type => typeof(TContext);
}