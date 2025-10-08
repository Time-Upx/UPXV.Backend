namespace UPXV.Backend.Common.Configuration;

public record ApplicationConfiguration
{
   public const string SECTION_NAME = "AppConfig";
   public required string ClientBaseURL { get; set; }

   public static ApplicationConfiguration Create (IServiceProvider provider)
   {
      return provider
            .GetRequiredService<IConfiguration>()
            .GetSection(SECTION_NAME)
            .Get<ApplicationConfiguration>()
            ?? throw Registry.ResolutionException<ApplicationConfiguration>();
   }
}
