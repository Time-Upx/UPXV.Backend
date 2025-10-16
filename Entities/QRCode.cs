using Microsoft.EntityFrameworkCore;
using UPXV.Backend.Data;

namespace UPXV.Backend.Entities;

public class QRCode : IHasRequirements
{
   public required string Id { get; set; }
   public int IntentId { get; set; }
   public Intent? Intent { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public DateTime CreatedAt { get; set; } = DateTime.Now;
   public DateTime? Expiration { get; set; }
   public bool HasExpired => DateTime.Now > Expiration;
   public string? Password { get; set; }
   public int? UsageLimit { get; set; }
   public int TimesUsed { get; set; }
   public bool HasReachedUsageLimit => TimesUsed > UsageLimit;
   public IDictionary<string, string> IntentParameters { get; set; } = new Dictionary<string, string>();

   public Attempt<string, Exception> GetUrl (string baseUrl)
   {
      if (Intent is null)
      {
         return new InvalidOperationException($"Intenção de id '{IntentId}' não foi carregada para o QR Code '{Id}'");
      }

      string url = baseUrl + Intent.UrlTemplate;

      List<Exception> errors = new();
      foreach (string requiredParameter in Intent.Parameters)
      {
         if (!IntentParameters.TryGetValue(requiredParameter, out string? parameter))
         {
            errors.Add(new ArgumentException($"Parâmetro '{requiredParameter}' não foi fornecido para o QR Code '{Id}'"));
         }
         if (parameter is null) continue;

         url.Replace("{" + requiredParameter + "}", parameter);
      }

      if (errors.Any())
      {
         return new AggregateException(errors);
      }

      return url;
   }

   public QRCode Clone (string id)
   {
      return new QRCode() 
      { 
         Id = id,
         IntentId = IntentId,
         Intent = Intent,
         Name = Name,
         Description = Description,
         Expiration = DateTime.Now + (Expiration - CreatedAt),
         Password = Password,
         UsageLimit = UsageLimit,
         TimesUsed = 0,
         IntentParameters = new Dictionary<string, string>(IntentParameters)
      };
   }
   public void LoadRequirements (DbContext context)
   {
      context.Load(this, qr => qr.Intent);
   }
}
