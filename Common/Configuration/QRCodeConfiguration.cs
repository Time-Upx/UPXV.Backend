namespace UPXV.Backend.Common.Configuration;

public record QRCodeConfiguration
{
   public const string SECTION_NAME = "QRCode";
   public int DefaultWidth { get; set; }
   public int DefaultHeight { get; set; }
   public int DefaultMargin { get; set; }
   public int DefaultQuality { get; set; }
   public string? ExportFolder { get; set; }

   public static QRCodeConfiguration Create(IServiceProvider provider)
   {
      return provider
            .GetRequiredService<IConfiguration>()
            .GetSection(SECTION_NAME)
            .Get<QRCodeConfiguration>()
            ?? throw Registry.ResolutionException<QRCodeConfiguration>();
   }
}
