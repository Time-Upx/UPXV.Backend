namespace UPXV.Backend.Common.Configuration;

public record FileConfiguration
{
   public required string TemporaryFolderPath { get; set; }
   public required string DestinationFolderBasePath { get; set; }

   public static FileConfiguration Create (IServiceProvider provider)
   {
      return provider
            .GetRequiredService<IConfiguration>()
            .Get<FileConfiguration>()
            ?? throw Registry.ResolutionException<FileConfiguration>();
   }
}
