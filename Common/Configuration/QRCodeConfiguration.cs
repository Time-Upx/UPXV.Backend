namespace UPXV.Backend.Common.Configuration;

public record QRCodeConfiguration
{
   public const string SECTION_NAME = "QRCode";
   public int DefaultWidth { get; set; } = 500;
   public int DefaultHeight { get; set; } = 500;
   public int DefaultMargin { get; set; } = 10;
   public required string DestinationPath { get; set; }
   public required string UrlExtension { get; set; }

   public string TrimExtension(string url) => url.Substring(0, url.Length - UrlExtension.Length - 1);
   public string AppendExtension (string url) => url + "/" + UrlExtension;

   public static QRCodeConfiguration Create(IServiceProvider provider)
   {
      return provider
            .GetRequiredService<IConfiguration>()
            .GetSection(SECTION_NAME)
            .Get<QRCodeConfiguration>()
            ?? throw Registry.ResolutionException<QRCodeConfiguration>();
   }
}
