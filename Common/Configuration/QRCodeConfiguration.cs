namespace UPXV.Backend.Common.Configuration;

public record QRCodeConfiguration
{
   public const string SECTION_NAME = "QRCode";
   public int DefaultWidth { get; set; } = 500;
   public int DefaultHeight { get; set; } = 500;
   public int DefaultMargin { get; set; } = 10;
   public int DefaultQuality { get; set; } = 100;

   public static QRCodeConfiguration Create(IServiceProvider provider)
   {
      return provider
            .GetRequiredService<IConfiguration>()
            .GetSection(SECTION_NAME)
            .Get<QRCodeConfiguration>()
            ?? throw Registry.ResolutionException<QRCodeConfiguration>();
   }
}
