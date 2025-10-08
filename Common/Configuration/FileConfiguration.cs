namespace UPXV.Backend.Common.Configuration;

public record FileConfiguration
{
   public const string SECTION_NAME = "Files";
   public required string TemporaryFolderPath { get; set; }
   public required string DestinationFolderBasePath { get; set; }

   public static FileConfiguration Create (IServiceProvider provider)
   {
      return provider
            .GetRequiredService<IConfiguration>()
            .GetSection(SECTION_NAME)
            .Get<FileConfiguration>()
            ?? throw Registry.ResolutionException<FileConfiguration>();
   }
}
