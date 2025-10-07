namespace UPXV.Backend.Common.Configuration;

public record QRCodeConfiguration
{
   public int DefaultWidth { get; set; } = 500;
   public int DefaultHeight { get; set; } = 500;
   public int DefaultMargin { get; set; } = 10;
   public required string DestinationPath { get; set; }

   public static QRCodeConfiguration Create(IServiceProvider provider)
   {
      return provider
            .GetRequiredService<IConfiguration>()
            .Get<QRCodeConfiguration>()
            ?? throw Registry.ResolutionException<QRCodeConfiguration>();
   }
}
